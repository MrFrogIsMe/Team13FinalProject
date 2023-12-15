using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Monster monster;
    public Camera mainCamera;
    public BlueprintSystem blueprintSystem;
<<<<<<< HEAD
    public Inventory inventory;
=======
>>>>>>> test
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;

    void Start()
    {
<<<<<<< HEAD
        inventory.setInv(player.resources);
=======
        
>>>>>>> test
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // left-click
        {
            if (buildingMode)
<<<<<<< HEAD
            {
                blueprintSystem.Build();
                inventory.setInv(player.resources);
            }
            else if (collectSystem.Collectable())
            {
                collectSystem.Collect();
                inventory.setInv(player.resources);
            }
=======
                blueprintSystem.Build();
            else if (collectSystem.Collectable())
                collectSystem.Collect();
>>>>>>> test
            else
                player.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildingMode = !buildingMode;
            blueprintSystem.ShowHotBar(buildingMode);
<<<<<<< HEAD
            inventory.ShowInv(buildingMode);

=======
>>>>>>> test
        }

        // Respawn(10f, 10f);
    }


    // The monster will spawn randomly in the area (-_x ~ _x, -_z ~ -z)
    void Respawn(float _x, float _z)
    {
        float x = Random.Range(-_x, _x);
        float z = Random.Range(-_z, _z);

        // Monster is the prefab to be instantiated
        Instantiate(monster, new Vector3(x, 1, z), Quaternion.identity);
    }
}
