using UnityEngine;

public class Tower : Entity
{
    void Start()
    {
        this.Setup();
<<<<<<< HEAD
        health = maxHealth;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
=======
        hp = maxHp;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);
>>>>>>> test
    }

    void Update()
    {
        if (health <= 0)
        {
            // game over
            Debug.Log("Game Over");
        }
    }

    public override void Move() { }

    public override void Attack() { }

    public override void Die() 
    {
        print("GameOver : Tower Is Down!");
    }
}
