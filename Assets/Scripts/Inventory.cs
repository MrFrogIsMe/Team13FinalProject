using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private BlueprintSystem blueprintSystem;
    [SerializeField] private InvSlot invslot;
    [SerializeField] private int paddingY;
    [SerializeField] private int paddingX;

    [SerializeField] private bool Inv = false;
    [SerializeField] private int slotPaddingY;
    [SerializeField] List<InvSlot> invSlots;

    [Header("InvAnimationUtil")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector2 appear;
    [SerializeField] private Vector2 disappear;
    [SerializeField] private float fade;
    [SerializeField] private float fadeDuration;

    //private float paddingYScaleFactor = 492.5f;
    //private float paddingXScaleFactor = 1113.5f;
    private int slotOffset = 99;
    public void resetInv() { 
        foreach (var slot in invSlots)
                Destroy(slot.gameObject);
        invSlots.Clear();
    
    }
    public void setInv(Dictionary <String, int> inv) {
        int count = 0;
        resetInv();
        foreach (var item in inv)
        {
            InvSlot NewObj = Instantiate(invslot);
            NewObj.GetComponent<RectTransform>().SetParent(transform.GetChild(0).transform);
            NewObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, - slotOffset - (slotPaddingY + 35) * count);
            NewObj.setContent(item.Key,item.Value);
            NewObj.GetComponent<RectTransform>().localScale = new Vector2(1,1);
            invSlots.Add(NewObj);
            count++;
        }

    }

    public void ShowInv(bool show) { Inv = show; }

    void Start()
    {
    }

    void Update()
    {
        /*float paddingX = paddingX_user * Mathf.Min( Screen.width / paddingXScaleFactor, 
            Screen.height/paddingYScaleFactor);

        float paddingY = paddingY_user * Mathf.Min( Screen.width / paddingXScaleFactor,
            Screen.height / paddingYScaleFactor);

        float remainingX = (Screen.width - blueprintSystem.getHotbarWidth()) / 2f - paddingX * 2;
        
        if (remainingX < 304){
            rect.sizeDelta = new Vector2( 303f, 
                Screen.height - paddingY * 2 - blueprintSystem.getHotbarHeight());

            appear = new Vector2(paddingX,
                blueprintSystem.getHotbarHeight() / 2f);

            disappear = new Vector2( -(rect.rect.width + paddingX), 
                blueprintSystem.getHotbarHeight() / 2f);
        }

        else { 
            rect.sizeDelta = new Vector2( remainingX, Screen.height - paddingY * 2);
            appear = new Vector2( paddingX ,0f);
            disappear = new Vector2(-(rect.rect.width + paddingX), 0f);
        };*/
        appear = new Vector2(paddingX, 0f);
        disappear = new Vector2(-310f, 0f);
        
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(appear, disappear, _curve.Evaluate(fade / fadeDuration));

        //for hotbar animation non-functional
        if (!Inv && fade + Time.deltaTime > fadeDuration)
        {
            fade = fadeDuration;
        }
        else if (Inv && fade - Time.deltaTime < 0)
        {
            fade = 0;
        }
        else if (!Inv)
        {
            fade += Time.deltaTime;
        }
        else
        {
            fade -= Time.deltaTime;
        }
    }
}
