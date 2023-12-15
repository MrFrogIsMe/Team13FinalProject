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

    [SerializeField] Slider slider;
    [SerializeField] text text;

    public void setExp(int expArg) {
        exp = expArg;
        slider.value = exp > maxExp ? maxExp : exp ;
    }
    public void setLevel(int levelArg, int maxExpArg) { 
        level = levelArg;
        maxExp = maxExpArg;
        slider.maxValue = maxExp;
        slider.value = 0;
        text.text = levelArg.ToString();
    }
}
