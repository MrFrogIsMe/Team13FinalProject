using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class HealthBarUtil : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private Text text;
    public Gradient gradient;
    public Image fill;

    void Update()
    {
        text.text = ((int)slider.value).ToString();
    }

    public void SetMaxHealth(int MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int Health)
    {
        slider.value = Health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
