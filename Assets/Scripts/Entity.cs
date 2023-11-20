using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float hp;
    public float damage;
    public float speed;

    public abstract void Move();
    public abstract void Attack();
    public abstract void TakeDamage(float damage);

    public void SetStatus(float hp = 100f, float damage = 10f, float speed = 10f)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
    }
}
