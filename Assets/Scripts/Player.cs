using UnityEngine;

public class Player : Entity
{
    public GameObject fireballSpawnpoint;
    public FireBall fireballTemplate;

    void Start()
    {
        this.Setup();
        maxHp = 100;
        hp = maxHp;
        hpBar.SetMaxHealth(maxHp);
        damage = 10;
        attackCD = 0.2f;
        maxSpeed = 10f;
        force = 10f;
        drag = 2f;
    }

    void Update()
    {
        this.Move();

        if (hp <= 0)
        {
            // game over
        }
    }

    public override void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);

        // Move by adding force
        // use sqrMagnitude to increace performance
        rb.AddForce(direction.normalized * force);
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public override void Attack()
    {
        // Shoot fireball
        fireballTemplate.transform.position = fireballSpawnpoint.transform.position;
        fireballTemplate.transform.forward = this.transform.forward;
        fireballTemplate.damage = this.damage;
        Instantiate(fireballTemplate);
    }
}
