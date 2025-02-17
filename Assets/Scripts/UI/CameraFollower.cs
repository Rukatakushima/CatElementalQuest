using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    private Vector3 playerVector;
    public int speed;

    private void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        playerVector = player.position;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.deltaTime);
    }
}
