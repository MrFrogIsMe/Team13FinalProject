using UnityEngine;

public class Tower : Entity
{
    void Start()
    {
        this.Setup();
        maxHp = 200;
        hp = maxHp;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            // game over
            Debug.Log("Tower hp = 0");
        }
    }

    public override void Move() { }

    public override void Attack() { }
}
