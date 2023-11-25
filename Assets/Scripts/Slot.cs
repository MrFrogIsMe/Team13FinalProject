using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Buliding _buliding;

    public void changeColor(Color color)
    {
        GetComponent<Image>().color = color;

    }
}
