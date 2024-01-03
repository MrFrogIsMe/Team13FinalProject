using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public HealthBar healthBar;
    public int attack;
    public float attackCD;
    public float maxSpeed;
    public float force;
    public float drag;
    protected Rigidbody rb;

    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();

    public virtual void TakeDamage(int damage)
    {
        health = health - damage < 0 ? 0 : health - damage;
        healthBar.SetHealth(health);
        // print($"Take damage: {damage}, hp: {hp}");
    }

    public void Setup()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rb = GetComponent<Rigidbody>();
    }
}
