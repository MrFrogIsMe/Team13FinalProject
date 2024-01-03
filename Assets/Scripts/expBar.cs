using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using text = TMPro.TextMeshProUGUI;

public class expBar : MonoBehaviour
{
    [SerializeField] int maxExp;
    [SerializeField] float exp;
    [SerializeField] int level;

    [SerializeField] Slider slider1;
    [SerializeField] text text;
    [SerializeField] RectTransform slider2;

    [Header("AnimationUtil")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float from;
    [SerializeField] private float to;
    [SerializeField] private float fade;
    [SerializeField] private float fadeDuration;

    public void setExp(int expArg) {
        from = exp;
        fade = 0;

        exp = expArg;
        slider2.GetComponent<Image>().fillAmount = exp / maxExp;
        to = exp;
    }
    public void setLevel(int levelArg, int maxExpArg) { 
        level = levelArg;
        maxExp = maxExpArg;
        
        slider1.maxValue = maxExp;
        text.text = levelArg.ToString();
    }

    private void Update()
    {
        if (fade + Time.deltaTime >= fadeDuration) {
            fade = fadeDuration;
        }
        else{
            fade += Time.deltaTime;
        }

        slider1.value = Mathf.Lerp(from, to, _curve.Evaluate(fade / fadeDuration));
    }
}
