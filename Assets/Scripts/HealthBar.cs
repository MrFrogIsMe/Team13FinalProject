using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Transform cam;
    [SerializeField] HealthBarUtil healthBarUtil;
    public void SetMaxHealth(int MaxHealth){
        healthBarUtil.SetMaxHealth(MaxHealth);
    }
    public void SetHealth(int Health){ 
        healthBarUtil.SetHealth(Health);
    }
        void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
