using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BlueprintSystem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Player playerT;
    [SerializeField] private Slot slot;
    [SerializeField] private List<Building> buildings;

    [Header("HotbarVisual")]
    [SerializeField] Color activeColor;
    [SerializeField] float slotPadding;

    [Header("State")]
    [SerializeField] private bool hotBar = false;
    [SerializeField] Building _toBuild;
    [SerializeField] int selectedSlot;
    [SerializeField] private List<Slot> slots;

    [Header("HotbarAnimationUtil")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector2 appear;
    [SerializeField] private Vector2 disappear;
    [SerializeField] private float fade;
    [SerializeField] private float fadeDuration;

    [Header("HotbarUtil")]
    [SerializeField] private float HotBarWidth;
    [SerializeField] private float scale;

    public bool IsHotBarShown() { return hotBar; }
    public void ShowHotBar(bool show) { hotBar = show; }
    public float getHotbarWidth() { return HotBarWidth; }
    public float getHotbarHeight() { return 100; } //to be changed
    public void Build()
    {
        if (!_toBuild.cannotBuild && _toBuild.enoughResource)
        {
            Vector3 position = playerT.transform.position + playerT.transform.forward * 2;
            position.y = buildings[selectedSlot].getGroundCoor();

            Quaternion rotation = Quaternion.Euler(
                buildings[selectedSlot].transform.eulerAngles.x, 
                playerT.transform.eulerAngles.y, buildings[selectedSlot].
                transform.eulerAngles.z);

            Instantiate(buildings[selectedSlot],position,rotation)
                .SetAsBuilding();

            foreach (var r in _toBuild.getRecipe())
            {
                playerT.resources[r.Key] -= r.Value;
            }
            Destroy(_toBuild.gameObject);
        }
    }

    void Start()
    {
        fade = hotBar ? 0f : 1f;
        float slotWidth = slot.GetComponent<RectTransform>().rect.width;
        HotBarWidth = buildings.Count * (slotWidth + slotPadding) - slotPadding;

        for (int i = 0; i < buildings.Count; i++)
        {
            Slot NewObj = Instantiate(slot);
            NewObj._buliding = buildings[i];
            slots.Add(NewObj);
            NewObj.GetComponent<RectTransform>().SetParent(transform);
            NewObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (slotWidth + slotPadding) - HotBarWidth / 2 + slotWidth / 2, 0f);

        }
    }

    void Update()
    {
        //update the UI to fit the window
        scale = Mathf.Min(Screen.height / 484f, Screen.width / 860f);
        GetComponent<RectTransform>().localScale = new Vector2(scale, scale);

        appear = new Vector2(0f, Screen.height / 484f * 70f);
        disappear = new Vector2(0f, Screen.height / 484f * -50f);

        /*appear = new Vector2(0f, 70f);
        disappear = new Vector2(0f, -50f);*/
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(appear, disappear, _curve.Evaluate(fade / fadeDuration));
        
        


        //for hotbar animation non-functional
        if (!hotBar && fade + Time.deltaTime > fadeDuration)
        {
            fade = fadeDuration;
        }
        else if (hotBar && fade - Time.deltaTime < 0)
        {
            fade = 0;
        }
        else if (!hotBar)
        {
            fade += Time.deltaTime;
        }
        else
        {
            fade -= Time.deltaTime;
        }
        //for hotbar animation end

        if (hotBar)
        {

            if (Input.GetAxis("Mouse ScrollWheel") > 0f && selectedSlot > 0)
            {
                selectedSlot--;
                if (_toBuild)
                    Destroy(_toBuild.gameObject);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && selectedSlot < buildings.Count - 1)
            {
                selectedSlot++;
                if (_toBuild)
                    Destroy(_toBuild.gameObject);
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                slots[i].changeColor(i == selectedSlot ? activeColor : Color.white);
            }

            Vector3 position = playerT.transform.position + playerT.transform.forward * 2;
            position.y = buildings[selectedSlot].getGroundCoor();

            Quaternion rotation = Quaternion.Euler(
                buildings[selectedSlot].transform.eulerAngles.x,
                playerT.transform.eulerAngles.y, buildings[selectedSlot].
                transform.eulerAngles.z);

            if (!_toBuild)
            {
                _toBuild = Instantiate(buildings[selectedSlot], position, rotation);
                _toBuild.enoughResource = true;

                foreach (var r in _toBuild.getRecipe()){
                    int k = -1;
                    playerT.resources.TryGetValue(r.Key, out k);
                    //Debug.Log(r.Key + ": " + k + " required: " + r.Value);
                    if (k == -1 || k < r.Value) { 
                    _toBuild.enoughResource = false;
                        break;
                    }
                }
                _toBuild.SetAsBlueprint();
            }
            else
                _toBuild.transform.position = position;
            _toBuild.transform.rotation = rotation;

        }
        else
        {
            if (_toBuild)
                Destroy(_toBuild.gameObject);
        }
    }
}