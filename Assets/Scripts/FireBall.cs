using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage;
    float speed = 50f;
    Vector3 movement;

    void Awake()
    {
        Destroy(this.gameObject, 2f);
    }

    void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        print($"hit: {other.tag}");
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Entity>().TakeDamage(damage);
            print($"{other.name}.hp = {other.GetComponent<Entity>().hp}");
        }
    }
}
