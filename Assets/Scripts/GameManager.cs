using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Player player;
    public MonsterSpawn[] spawnPoints = new MonsterSpawn[5];
    public Camera mainCamera;
    public BlueprintSystem blueprintSystem;
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;

    int round;

    void Start()
    {
        StartCoroutine(RoundManager());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // left-click
        {
            if (buildingMode)
            {
                blueprintSystem.Build();
            }
            else if (collectSystem.Collectable())
            {
                collectSystem.Collect();
            }
            else
            {
                player.Attack();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildingMode = !buildingMode;
            blueprintSystem.ShowHotBar(buildingMode);
        }

    }

    IEnumerator RoundManager()
    {
        // round 0
        round = 0;
        Debug.Log("Round " + round);

        for (int i = 0; i < 5; i++)
        {
            // (type, num, cd)
            spawnPoints[i].SpawnMonsters(0, 2, 5);
        }

        yield return new WaitForSeconds(40);

        // round 1
        round = 1;
        Debug.Log("Round " + round);

        // 5 spawn points
        for (int i = 0; i < 5; i++)
        {
            // (type, num, cd)
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }

        yield return new WaitForSeconds(60);

        Debug.Log("Time's up");
    }
}