using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Monster monster;
    public float cooldown;
    public float count;
    public bool enableSpawning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSpawning) {
            if (count + Time.deltaTime >= cooldown)
            {
                count = 0;
                Instantiate(monster);
            }
            else
                count += Time.deltaTime;
        }
        
    }
}
