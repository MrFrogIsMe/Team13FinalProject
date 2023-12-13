using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Building _buliding;

    public void changeColor(Color color)
    {
        GetComponent<Image>().color = color;

    }
}
