using UnityEngine;

public class Resource : MonoBehaviour 
{
    public int health = 3;
    HealthBar healthBar;

    void Start() 
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    public void Collect()
    {
        --health;
        healthBar.SetHealth(health);
    }
}
