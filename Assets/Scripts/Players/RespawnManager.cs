using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;

    public void Respawn(GameObject obj)
    {
        obj.transform.position = respawnPoint.position;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        Debug.Log("Игрок респавнится!");
    }
}