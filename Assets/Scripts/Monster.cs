using UnityEngine;

public class Monster : Entity
{
    public Tower tower;

    void Start()
    {
        tower = FindObjectOfType<Tower>();
        this.Setup();
        maxHp = 20;
        hp = maxHp;
        healthBar = this.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);
        damage = 5;
        attackCD = 0.5f;
        maxSpeed = 5f;
        force = 100f;
        drag = 2f;
    }

    void Update()
    {
        this.Move();

        // Check if the monster is alive
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    public override void Move()
    {
        // The monster moves toward the origin at a constant speed
        Vector3 direction = tower.transform.position - this.transform.position;
        direction.y = 0f;
        this.transform.forward = direction;
        rb.AddForce(direction.normalized * force);
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed * Time.deltaTime;
        }
    }

    public override void Attack()
    {
        ;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Tower"))
        {
            maxSpeed = 0f;

            // Deals damage to the obstacles in its way
            this.Attack();
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Tower"))
        {
            maxSpeed = 10f;
        }
    }
}
