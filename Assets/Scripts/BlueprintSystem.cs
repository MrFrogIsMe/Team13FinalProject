using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BlueprintSystem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Transform playerT;
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

    public bool IsHotBarShown() { return hotBar; }
    public void ShowHotBar(bool show) { hotBar = show; }
    public void Build()
    {
        if (!_toBuild.cannotBuild)
        {
            Instantiate(buildings[selectedSlot], playerT.position + playerT.forward * 2, playerT.rotation)
                .SetAsBuilding();
        }
    }

    void Start()
    {
        fade = hotBar ? 0f : 1f;
        float slotWidth = slot.GetComponent<RectTransform>().rect.width;
        float width = buildings.Count * (slotWidth + slotPadding) - slotPadding;

        for (int i = 0; i < buildings.Count; i++)
        {
            Slot NewObj = Instantiate(slot);
            NewObj._buliding = buildings[i];
            slots.Add(NewObj);
            NewObj.GetComponent<RectTransform>().SetParent(transform);
            NewObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (slotWidth + slotPadding) - width / 2 + slotWidth / 2, 0f);

        }
    }

    void Update()
    {
        //update the UI to fit the window
        float scale = Mathf.Min(Screen.height / 484f, Screen.width / 860f);
        GetComponent<RectTransform>().localScale = new Vector2(scale, scale);

        appear = new Vector2(0f, Screen.height / 484f * 70f);
        disappear = new Vector2(0f, Screen.height / 484f * -50f);
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

            if (!_toBuild)
            {
                _toBuild = Instantiate(buildings[selectedSlot], playerT.position + playerT.forward * 2, playerT.rotation);
                _toBuild.SetAsBlueprint();
            }
            else
                _toBuild.transform.position = playerT.position + playerT.forward * 2;
            _toBuild.transform.rotation = Quaternion.Euler(0f, playerT.transform.eulerAngles.y, 0f);

        }
        else
        {
            if (_toBuild)
                Destroy(_toBuild.gameObject);
        }
    }
}