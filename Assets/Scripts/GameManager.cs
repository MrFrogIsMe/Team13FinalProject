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
    public const int maxRound = 3;
    // the amount of monsters spawn in each spawn point for each round
    public int[] monsterAmount = new int[maxRound];
    // the cooldown for monster spawning for each round
    public float[] spawnCD = new float[maxRound];
    // the time length for each round
    public float[] Time = new float[maxRound];

    void Start()
    {
        round = 0;

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
        while (round < maxRound)
        {
            Debug.Log("Round " + round);

            // 5 spawn points
            for (int i = 0; i < 5; i++)
            {
                spawnPoints[i].SpawnMonsters(monsterAmount[round], spawnCD[round]);
            }

            yield return new WaitForSeconds(Time[round]);

            round++;
        }
    }
}
