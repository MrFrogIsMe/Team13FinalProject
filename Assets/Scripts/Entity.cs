using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public HealthBar hpBar;
    public int damage;
    public float attackCD;
    public float maxSpeed;
    public float force;
    public float drag;
    public Rigidbody rb;

    public abstract void Move();
    public abstract void Attack();

    public void TakeDamage(int damage)
    {
        hp = hp - damage < 0 ? 0 : hp - damage;
        hpBar.SetHealth(hp);
    }

    public void Setup()
    {
        hpBar = GetComponentInChildren<HealthBar>();
        rb = GetComponent<Rigidbody>();
    }
}
