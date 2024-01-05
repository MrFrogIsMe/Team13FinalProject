using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage;
    float speed = 50f;
    Vector3 movement;

    public AudioClip mySoundClip;



    public GameObject ExplosionPrefab;
    public DamageNum damageNum;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 2f);

    }

    void Update()
    {
        
        rb.velocity = transform.forward.normalized * speed;
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = mySoundClip;
        audioSource.Play();

        // print($"hit: {other.tag}");
        if (other.CompareTag("Monster"))

        {
            // print($"{other.gameObject}");
            other.GetComponent<Monster>().TakeDamage(damage);
            Instantiate(damageNum, this.transform.position, Quaternion.identity)
                .setNumber(damage);

            // print($"{other.name}.hp = {other.GetComponent<Entity>().hp}");
        }
    }
}
