using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilitySystem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Player playerT;
    [SerializeField] private Slot_Ability slot;
    [SerializeField] private List<Ability> abilities;
    [SerializeField] Sprite fallbackImage;

    [Header("HotbarVisual")]
    [SerializeField] float slotPadding;

    [Header("State")]
    [SerializeField] private bool hotBar = false;
    [SerializeField] int selectedSlot;
    private List<Slot_Ability> slots;

    [Header("HotbarAnimationUtil")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector2 appear;
    [SerializeField] private Vector2 disappear;
    [SerializeField] private float fade;
    [SerializeField] private float fadeDuration;

    [Header("HotbarUtil")]
    [SerializeField] private float HotBarWidth;
    [SerializeField] private float scale;

    const float playerGroundCoor = -1.5f;

    public bool IsHotBarShown() { return hotBar; }
    public void ShowHotBar(bool show) { hotBar = show; }
    public float getHotbarWidth() { return HotBarWidth; }
    public float getHotbarHeight() { return 100; } //to be changed
    public void Upgrade()
    {

    }

    void Start()
    {
        slots = new List<Slot_Ability>{};
        fade = hotBar ? 0f : 1f;
        float slotWidth = slot.GetComponent<RectTransform>().rect.width;
        HotBarWidth = abilities.Count * (slotWidth + slotPadding) - slotPadding;

        for (int i = 0; i < abilities.Count; i++)
        {
            Slot_Ability NewObj = Instantiate(slot);
            
            NewObj._ability = abilities[i];
            slots.Add(NewObj);
            NewObj.GetComponent<RectTransform>().SetParent(transform);
            NewObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (slotWidth + slotPadding) - HotBarWidth / 2 + slotWidth / 2, 0f);

            NewObj.setName(abilities[i].name);
            NewObj.setImage(abilities[i].abilityImage ? abilities[i].abilityImage : fallbackImage);

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
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && selectedSlot < abilities.Count - 1)
            {
                selectedSlot++;
            }

            for (int i = 0; i < abilities.Count; i++)
            {
                slots[i].selectThis(i == selectedSlot);
            }

        }
        else
        {
        }
    }
}
