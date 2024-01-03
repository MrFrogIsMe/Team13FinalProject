using System;
using System.Collections;
using UnityEngine;
// using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Player player;
    public MonsterSpawn[] spawnPoints = new MonsterSpawn[5];
    public Camera mainCamera;
    //public BlueprintSystem blueprintSystem;
    public AbilitySystem AbilitySystem;
    public Inventory inventory;
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;
    public string[] scenes = { "Scene1", "Scene2", "Scene3" };
    public int currentScene = 0;

    float timeRemaining;
    Action[] roundFunctions;
    float[] roundTime;

    public Wave_Counter waveCounter;
    int round;

    void Start()
    {
        roundFunctions = new Action[]
        {
            RoundZero, RoundOne, RoundTwo, RoundThree, RoundFour,
            RoundFive, RoundSix, RoundSeven, RoundEight, RoundNine, RoundTen
        };

        roundTime = new float[]
        {
            20, 40, 40, 40, 40,
            40, 40, 40, 40, 40, 40
        };
        inventory.setInv(player.resources);
        round = 0;

        StartCoroutine(RoundManager());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // left-click
        {
            if (buildingMode)
            {
                //blueprintSystem.Build();
                //inventory.setInv(player.resources);
                player.Attack();
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
            //blueprintSystem.ShowHotBar(buildingMode);
            //AbilitySystem.ShowHotBar(buildingMode);
            inventory.ShowInv(buildingMode);
        }

    }

//     public void SceneManager()
//     {
//         if (currentScene < scenes.Length)
//         {
//             SceneManager.LoadScene(scenes[currentScene]);
//             currentScene++;
//         }
//     }

    IEnumerator RoundManager()
    {
        while (round < 10)
        {
            Debug.Log("Round " + round);
            // invoke corresponding function for each round
            roundFunctions[round]?.Invoke();

            // countdown
            timeRemaining = roundTime[round];
            while (timeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                timeRemaining -= 1;
            }
            round++;
            waveCounter.setNum(round);
        }
    }

    void RoundZero()
    {
        for (int i = 0; i < 5; i++)
        {
            // spawnPoints[i].SpawnMonsters(0, 2, 5);
        }
    }

    void RoundOne()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
        }
    }

    void RoundTwo()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 3, 3);
            spawnPoints[i].SpawnMonsters(1, 1, 1);
        }
    }

    void RoundThree()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 2);
            spawnPoints[i].SpawnMonsters(1, 2, 3);
            spawnPoints[i].SpawnMonsters(2, 1, 5);
        }
    }

    void RoundFour()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(1, 2, 3);
            spawnPoints[i].SpawnMonsters(2, 2, 4);
            spawnPoints[i].SpawnMonsters(3, 2, 5);
        }
    }

    void RoundFive()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(1, 2, 2);
            spawnPoints[i].SpawnMonsters(2, 2, 4);
            spawnPoints[i].SpawnMonsters(3, 2, 5);
        }
        spawnPoints[0].SpawnMonsters(4, 1, 1);
    }

    void RoundSix()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 5, 1);
            spawnPoints[i].SpawnMonsters(5, 1, 1);
        }
    }

    void RoundSeven()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(5, 3, 3);
            spawnPoints[i].SpawnMonsters(6, 1, 1);
        }
    }

    void RoundEight()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(2, 3, 4);
            spawnPoints[i].SpawnMonsters(3, 3, 3);
            spawnPoints[i].SpawnMonsters(4, 1, 5);
        }
    }

    void RoundNine()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(5, 2, 3);
            spawnPoints[i].SpawnMonsters(6, 3, 5);
        }
    }

    void RoundTen()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(5, 2, 3);
            spawnPoints[i].SpawnMonsters(6, 2, 5);
        }
        spawnPoints[0].SpawnMonsters(7, 1, 1);
    }
}
