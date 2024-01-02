using UnityEngine;
using UnityEngine.UI;
using text = TMPro.TextMeshProUGUI;

[System.Serializable]
public class Slot_Ability : MonoBehaviour
{
    public Ability _ability;

    [Header("Background")]
    public Color selected_Color;
    public Color selected_cooldown_Color;
    public Color unselected_Color;
    public Color unselected_cooldown_Color;
    public Color active_Color;

    [Header("Text")]
    public Color selected_text_Color;
    public Color unselected_text_Color;

    [Header("Image")]
    public Color selected_image_Color;
    public Color selected_image_cooldown_Color;
    public Color unselected_image_Color;
    public Color unselected_image_cooldown_Color;
    public Color active_image_Color;

    bool this_Selected;
    [SerializeField] text cooldown_text;
    [SerializeField] text name_text;
    [SerializeField] Image image;

    public void selectThis(bool selected)
    {
        this_Selected = selected;

    }

    public void setName(string name) { 
        name_text.text = name;
    }

    public void setImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    void Update() {
        if (this_Selected)
        {
            name_text.color = selected_text_Color;
            if (_ability.state == AbilityState.ready)
            {
                image.color = selected_image_Color;
                GetComponent<Image>().color = selected_Color;
                cooldown_text.enabled = false;
            }
            else //if (_ability.state == AbilityState.cooldown)
            {
                image.color = selected_image_cooldown_Color;
                GetComponent<Image>().color = selected_cooldown_Color;
                cooldown_text.enabled = (_ability.state == AbilityState.cooldown);
                cooldown_text.text = _ability.getCurrCooldownTime().ToString();
            }
        }
        else {
            name_text.color = unselected_text_Color;
            if (_ability.state == AbilityState.ready)
            {
                image.color = unselected_image_Color;
                GetComponent<Image>().color = unselected_Color;
                cooldown_text.enabled = false;
            }
            else //if (_ability.state == AbilityState.cooldown)
            {
                image.color = unselected_image_cooldown_Color;
                GetComponent<Image>().color = unselected_cooldown_Color;
                cooldown_text.enabled = (_ability.state == AbilityState.cooldown);
                cooldown_text.text = _ability.getCurrCooldownTime().ToString();
            }
        }
    }
}
