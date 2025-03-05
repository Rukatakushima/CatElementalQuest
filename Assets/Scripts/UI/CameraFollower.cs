using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    private Vector3 playerVector;
    public int zPosition = -10;
    public int speed;

    private void Start() => player = GameObject.FindGameObjectWithTag("Player").transform;

    private void Update() => UpdateCameraPosition();

    private void UpdateCameraPosition()
    {
        playerVector = player.position;
        playerVector.z = zPosition;
        transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.deltaTime);
    }
}
