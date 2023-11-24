using UnityEngine;

public class Monster : Entity
{
    public Tower tower;

    void Start()
    {
        tower = FindObjectOfType<Tower>();
        this.SetStatus(20f, 10f, 10f);
    }

    void Update()
    {
        this.Move();

        // Check if the monster is alive
        if (hp < 0)
        {
            Destroy(gameObject);
        }

    }

    public override void Move()
    {
        // The monster moves toward the origin at a constant speed
        Vector3 direction = tower.transform.position - this.transform.position ;
        direction.y = 0f;
        this.transform.forward = direction;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public override void Attack()
    {
        ;
    }

    public override void TakeDamage(float damage)
    {
        hp -= damage;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Tower"))
        {
            speed = 0f;

            // Deals damage to the obstacles in its way
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Tower"))
        {
            speed = 10f;
        }
    }
}
