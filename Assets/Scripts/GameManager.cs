using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Camera mainCamera;

    void Start()
    {
        player.SetStatus();
    }

    void Update()
    {
        player.Move();
        TrackMouseRotation();
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
}
