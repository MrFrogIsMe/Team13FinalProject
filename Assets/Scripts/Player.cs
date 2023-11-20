using UnityEngine;

public class Player : Entity
{
    public override void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        this.transform.position += movement * speed * Time.deltaTime;

        // Move by adding force
        // GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    public override void Attack()
    {
        ;
    }

    public override void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
