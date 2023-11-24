using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float hp;
    public float damage;
    public float speed;
    public float attackCD;

    public abstract void Move();
    public abstract void Attack();

    public void TakeDamage(float damage)
    {
        this.hp -= damage;
    }

    public void SetStatus(float hp = 100f, float damage = 10f, float speed = 10f, float attackCD = 0.5f)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.attackCD = attackCD;
    }
}
