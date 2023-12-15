using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public HealthBar healthBar;
    public int damage;
    public float attackCD;
    public float maxSpeed;
    public float force;
    public float drag;
    protected Rigidbody rb;

    public abstract void Move();
    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
        hp = hp - damage < 0 ? 0 : hp - damage;
        healthBar.SetHealth(hp);
        // print($"Take damage: {damage}, hp: {hp}");
    }

    public void Setup()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rb = GetComponent<Rigidbody>();
    }
}
