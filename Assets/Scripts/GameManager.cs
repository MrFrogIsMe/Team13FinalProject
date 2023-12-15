using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Monster monster;
    public Camera mainCamera;
    public BlueprintSystem blueprintSystem;
    public Inventory inventory;
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;

    void Start()
    {
        inventory.setInv(player.resources);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // left-click
        {
            if (buildingMode)
            {
                blueprintSystem.Build();
                inventory.setInv(player.resources);
            }
            else if (collectSystem.Collectable())
            {
                collectSystem.Collect();
                inventory.setInv(player.resources);
            }
            else
                player.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildingMode = !buildingMode;
            blueprintSystem.ShowHotBar(buildingMode);
            inventory.ShowInv(buildingMode);

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
