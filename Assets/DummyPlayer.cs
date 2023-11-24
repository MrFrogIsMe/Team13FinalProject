using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public HealthBar healthBar;
    [SerializeField] private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) { 
            TakeDamage();
        }
        if (Input.GetKeyDown("e"))
        {
            Regen();
        }
        if (Input.GetKey("w"))
        {
            transform.position += Vector3.ProjectOnPlane(cam.forward,Vector3.up) * 10 * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position -= Vector3.ProjectOnPlane(cam.right, Vector3.up) * 10 * Time.deltaTime;

        }
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.ProjectOnPlane(cam.right, Vector3.up) * 10 * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= Vector3.ProjectOnPlane(cam.forward, Vector3.up) * 10 * Time.deltaTime;
        }
    }

    void TakeDamage(int damage = 10) {
        health = health - damage < 0 ? 0 : health - damage;
        healthBar.SetHealth(health);
    }

    void Regen(int restore = 10)
    {
        health = health + restore > maxHealth ? maxHealth : health + restore;
        healthBar.SetHealth(health);
    }
}
