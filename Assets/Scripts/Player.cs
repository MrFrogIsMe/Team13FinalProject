using UnityEngine;

public class Player : Entity
{
    public GameObject fireballSpawnpoint;
    public FireBall fireballTemplate;

    void Start()
    {
        this.SetStatus(100f, 10f, 10f, 0.2f);
    }

    void Update()
    {
        this.Move();
    }

    public override void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);

        this.transform.position += direction.normalized * speed * Time.deltaTime;

        // Move by adding force
        // GetComponent<Rigidbody>().AddForce(movement * speed);
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
