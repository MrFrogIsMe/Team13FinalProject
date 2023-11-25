using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class BlueprintSystem : MonoBehaviour
{
    [SerializeField] private List<Buliding> buildings;
    [SerializeField] private List<Slot> slots;
    [SerializeField] private Slot slot;
    [SerializeField] int selectedSlot;
    [SerializeField] Color activeColor;

    [SerializeField] Transform playerT;
    [SerializeField] Buliding _toBuild;
    [SerializeField] Material previewMat;

    public bool showHotBar = false;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector2 appear;
    [SerializeField] private Vector2 disappear;
    [SerializeField] private float fade;
    [SerializeField] private float fadeDuration;
    void Start()
    {
        fade = showHotBar ? 0f : 1f;
        int width = buildings.Count * 110 - 10;

        for (int i = 0; i < buildings.Count; i++)
        { 
            Slot NewObj = Instantiate(slot);
            NewObj._buliding = buildings[i];
            slots.Add(NewObj);
            NewObj.GetComponent<RectTransform>().SetParent(transform);
            NewObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 110 - width/2 + 50, -183f);
            
        }
    }
    void Update()
    {
        //for hotbar animation non-functional
        if (!showHotBar && fade + Time.deltaTime > fadeDuration)
        {
            fade = fadeDuration;
        }
        else if (showHotBar && fade - Time.deltaTime < 0)
        {
            fade = 0;
        }
        else if (!showHotBar)
        {
            fade += Time.deltaTime;
        }
        else { 
            fade -= Time.deltaTime;
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(appear,disappear,_curve.Evaluate( fade/fadeDuration));

        if (Input.GetMouseButtonDown(1)) {
            if (showHotBar)
            {
                showHotBar = false;
            }
            else { 
                showHotBar = true;
            }
        }
        //for hotbar animation end

        if (showHotBar){

            for (int i = 0; i < buildings.Count; i++)
            {
                slots[i].changeColor(i == selectedSlot ? activeColor : Color.white);
            }

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

            if (!_toBuild) { 
                _toBuild = Instantiate(buildings[selectedSlot], playerT.position + playerT.forward * 2 , playerT.rotation);
                _toBuild.GetComponent<MeshRenderer>().material = previewMat;
            }
            else
                _toBuild.transform.position = playerT.position + playerT.forward * 2;
                _toBuild.transform.rotation = playerT.rotation;

        }
        else
        {
            if(_toBuild)
                Destroy(_toBuild.gameObject);
        }
    }
}
