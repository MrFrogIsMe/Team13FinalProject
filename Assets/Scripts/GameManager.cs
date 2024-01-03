using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public MonsterSpawn[] spawnPoints = new MonsterSpawn[5];
    public Camera mainCamera;
    public BlueprintSystem blueprintSystem;
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;
    public string[] scenes = { "Scene1", "Scene2", "Scene3" };
    public int currentScene = 0;

    int round;
    float timeRemaining;
    Action[] roundFunctions;
    float[] roundTime;

    void Start()
    {
        roundFunctions = new Action[]
        {
            RoundZero, RoundOne, RoundTwo, RoundThree, RoundFour,
            RoundFive, RoundSix, RoundSeven, RoundEight, RoundNine
        };

        roundTime = new float[]
        {
            20, 40, 40, 40, 40,
            40, 40, 40, 40, 40
        };

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

    public void SceneManager()
    {
        if (currentScene < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[currentSceneIndex]);
            currentSceneIndex++;
        }
    }

    IEnumerator RoundManager()
    {
        int round = 0;

        while (round < 10)
        {
            Debug.Log("Round: " + round);

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
        }
    }

    void RoundZero()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
        }
    }

    void RoundOne()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundTwo()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundThree()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundFour()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundFive()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundSix()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundSeven()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundEight()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }

    void RoundNine()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPoints[i].SpawnMonsters(0, 2, 5);
            spawnPoints[i].SpawnMonsters(1, 1, 0);
        }
    }
}