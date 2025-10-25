using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // drag your Player here
    private Vector3 offset;

    void Start()
    {
        // calculate initial offset between camera and player
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Debug.Log("Camera updating");  // should print every frame
        transform.position = player.transform.position + offset;
    }
}