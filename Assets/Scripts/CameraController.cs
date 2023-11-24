using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    Vector3 offset = new Vector3(0, 10.03f, -8.72f);
    float moveSpeed = 10f;

    void Update()
    {
        playerPos = player.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + offset, moveSpeed * Time.deltaTime);
        // print(this.transform.position - playerPos);
    }
}
