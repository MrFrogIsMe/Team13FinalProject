using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;
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
    public void SpawnMonsters(int _num, float cd)
    {
        num = 0;

        StartCoroutine(Spawn(_num, cd));
    }

    IEnumerator Spawn(int _num, float cd)
    {
        while (num < _num)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            monster.transform.parent = monsters;

            num++;

            yield return new WaitForSeconds(cd);
        }
    }
}
