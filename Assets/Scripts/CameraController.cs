using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    Vector3 offset = new Vector3(0, 11.46f, -2.13f);
    float moveSpeed = 10f;

    void Update()
    {
        playerPos = player.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + offset, moveSpeed * Time.deltaTime);
    }
}
