using UnityEngine;

public abstract class Entity : MonoBehaviour
{
<<<<<<< HEAD
    public int health;
    public int maxHealth;
    public HealthBar healthBar;
    public int attack;
=======
    public int hp;
    public int maxHp;
    public HealthBar healthBar;
    public int damage;
>>>>>>> test
    public float attackCD;
    public float maxSpeed;
    public float force;
    public float drag;
    protected Rigidbody rb;

    public abstract void Move();
    public abstract void Attack();
    public abstract void Die();

    public void TakeDamage(int damage)
    {
<<<<<<< HEAD
        health = health - damage < 0 ? 0 : health - damage;
        healthBar.SetHealth(health);
=======
        hp = hp - damage < 0 ? 0 : hp - damage;
        healthBar.SetHealth(hp);
>>>>>>> test
        // print($"Take damage: {damage}, hp: {hp}");
    }

    public void Setup()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rb = GetComponent<Rigidbody>();
    }
}
