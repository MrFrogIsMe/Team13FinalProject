using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Monster monster;
    public Camera mainCamera;
    public BlueprintSystem blueprintSystem;
    public bool buildingMode;
    public CollectResourceSystem collectSystem;
    public bool collectMode;

    void Start()
    {
        buildingMode = false;
        blueprintSystem.ShowHotBar(buildingMode);
    }

    void Update()
    {
        TrackMouseRotation();

        if (Input.GetMouseButtonDown(0))    // left-click
        {
            if (buildingMode)
                blueprintSystem.Build();
            else if (collectSystem.Collectable())
                collectSystem.Collect();
            else
                player.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildingMode = !buildingMode;
            blueprintSystem.ShowHotBar(buildingMode);
        }

        // Respawn(10f, 10f);
    }

    void TrackMouseRotation()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            player.transform.LookAt(new Vector3(pointToLook.x, player.transform.position.y, pointToLook.z));
        }
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
