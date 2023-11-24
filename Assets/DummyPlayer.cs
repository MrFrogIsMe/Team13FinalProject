using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public int health;
    [SerializeField] private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * 10 * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0,-90,0) * Time.deltaTime);

        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * 10 * Time.deltaTime;
        }
    }
}
