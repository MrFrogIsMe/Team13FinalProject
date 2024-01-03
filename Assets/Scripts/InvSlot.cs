using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class InvSlot : MonoBehaviour
{
    public String Name;
    public int Quantity;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void setContent(String s, int q) {
        Name = s;
        Quantity = q;
        transform.GetChild(1).gameObject.GetComponent<Text>().text = s;
        transform.GetChild(2).gameObject.GetComponent<Text>().text = q.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
