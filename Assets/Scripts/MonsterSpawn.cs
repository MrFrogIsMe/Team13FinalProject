using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Monster monsterPrefab;
    public float cooldown;
    public float count;
    public bool enableSpawning;
    public Transform monsters;
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
                Monster monster = Instantiate(monsterPrefab, transform.position,    Quaternion.identity);
                monster.transform.parent = monsters;
            }
            else
                count += Time.deltaTime;
        }
        
    }
}
