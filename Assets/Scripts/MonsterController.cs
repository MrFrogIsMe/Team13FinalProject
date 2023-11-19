using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] float maxHP = 1000f;
    public float currHP = 0f;
    public float damage = 50f;

    [SerializeField] float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the monster is alive
        if (currHP <= 0) {
            Destroy(gameObject);
        }

        // The monster moves toward the origin at a constant speed
        Vector3 direction = -transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") || collision.gameObject.CompareTag("Tower")) {
            speed = 0f;

            // Deals damage to the obstacles in its way
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") || collision.gameObject.CompareTag("Tower")) {
            speed = 1f;
        }
    }

    /*
    // This function should be put in the GameManager
    // The monster will spawn randomly in the area (-_x ~ _x, -_z ~ -z)
    void respawn(float _x, float _z)
    {
        float x = Random.Range(-_x, _x);
        float z = Random.Range(-_z, _z);
        // Monster is the prefab to be instantiated
        Instantiate(Monster, new Vector3(x, 0, z), Quaternion.identity);
    }
    */
}
