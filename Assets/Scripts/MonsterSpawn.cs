using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject[] MonsterPrefabs = new GameObject[2];
    public Transform monsters;

    // the number of monsters spawned so far
    int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn a monster every cd seconds until there are _num monsters
    public void SpawnMonsters(int type, int _num, float cd)
    {
        num = 0;

        StartCoroutine(Spawn(type, _num, cd));
    }

    // Spawn _num monsters with cooldown = cd
    IEnumerator Spawn(int type, int _num, float cd)
    {
        while (num < _num)
        {
            GameObject monster = Instantiate(MonsterPrefabs[type], transform.position, Quaternion.identity);
            monster.transform.parent = monsters;

            num++;

            yield return new WaitForSeconds(cd);
        }
    }
}
