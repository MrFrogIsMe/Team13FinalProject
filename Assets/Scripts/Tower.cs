using UnityEngine;
using UnityEngine.SceneManagement;
public class Tower : Entity
{
    void Start()
    {
        this.Setup();
        health = maxHealth;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (health <= 0)
        {
            // game over
            SceneManager.LoadScene("Over");
        }
    }

    public override void Move() { }

    public override void Attack() { }

    public override void Die() 
    {
        
        print("GameOver : Tower Is Down!");
    }
}
