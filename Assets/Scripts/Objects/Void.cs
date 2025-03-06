using UnityEngine;

public class Void : MonoBehaviour
{
    private PlayerSpawner playerSpawner;

    private void Awake() => playerSpawner = FindObjectOfType<PlayerSpawner>();

    private void OnCollisionEnter2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            playerSpawner.Respawn(playerAbility.gameObject);

        Debug.Log("Игрок " + playerAbility + " дебоширит");
    }
}
