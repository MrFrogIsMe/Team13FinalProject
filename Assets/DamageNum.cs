using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using text = TMPro.TextMeshProUGUI;

public class DamageNum : MonoBehaviour
{
    public Transform cam;
    [SerializeField] int life;
    [SerializeField] float age;

    public text text;

    void Start()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Transform>();
        age = 0;
    }

    public void setNumber(int num) { 
        text.text = num.ToString();
    }

    void Update()
    {
        age += Time.deltaTime;
        if (age > life) 
            Destroy(this.gameObject);
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
