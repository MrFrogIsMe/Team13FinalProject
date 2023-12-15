using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage;
    float speed = 50f;
    Vector3 movement;
    Rigidbody rb;



    public GameObject ExplosionPrefab;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 2f);

    }

    void Update()
    {
<<<<<<< HEAD
        rb.velocity = transform.forward.normalized * speed;
=======
        
        rb.velocity = transform.forward.normalized * speed;
        
>>>>>>> test
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
<<<<<<< HEAD
        // print($"hit: {other.tag}");
        if (other.CompareTag("Monster"))
        {
            // print($"{other.gameObject}");
            other.GetComponent<Monster>().TakeDamage(damage);
=======
        Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);

        // print($"hit: {other.tag}");
        if (other.CompareTag("Monster"))

        {
            // print($"{other.gameObject}");
            other.GetComponent<Monster>().TakeDamage(damage);

>>>>>>> test
            // print($"{other.name}.hp = {other.GetComponent<Entity>().hp}");
        }
    }
}
