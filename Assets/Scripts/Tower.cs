using UnityEngine;

public class Tower : Entity
{
    void Start()
    {
        this.Setup();
        hp = maxHp;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            // game over
            Debug.Log("Game Over");
        }
    }

    public override void Move() { }

    public override void Attack() { }
}
