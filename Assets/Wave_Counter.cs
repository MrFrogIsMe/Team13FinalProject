using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using text = TMPro.TextMeshProUGUI;

public class Wave_Counter : MonoBehaviour
{
    public text wave_num;

    public void setNum(int num) {
        wave_num.text = num.ToString();
    }
}
