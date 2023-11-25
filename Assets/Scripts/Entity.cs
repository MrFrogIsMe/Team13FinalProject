using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public HealthBar hpBar;
    public int damage;
    public float speed;
    public float attackCD;

    public abstract void Move();
    public abstract void Attack();

    public void TakeDamage(int damage)
    {
        hp = hp - damage < 0 ? 0 : hp - damage;
        hpBar.SetHealth(hp);
    }

    public void SetStatus(int maxHp = 100, int damage = 10, float speed = 10f, float attackCD = 0.5f)
    {
        this.maxHp = maxHp;
        this.hp = maxHp;
        hpBar = this.GetComponentInChildren<HealthBar>();
        hpBar.SetMaxHealth(maxHp);

        this.damage = damage;
        this.speed = speed;
        this.attackCD = attackCD;
    }
}
