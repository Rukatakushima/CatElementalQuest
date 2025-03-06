using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AudioSource))]
public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    [SerializeField] protected AudioClip spawnSound;
    protected AudioSource audioSource;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void Start() => SpawnPlayer();

    public void SpawnPlayer()
    {
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, transform.position, Quaternion.identity);
    }

    public void Respawn(GameObject obj)
    {
        PlayRespawnSound();
        obj.transform.position = transform.position;
        ResetRespawnedObjectVelocity(obj);
    }

    private void ResetRespawnedObjectVelocity(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;
    }

    private void PlayRespawnSound()
    {
        if (audioSource != null && spawnSound != null)
            audioSource.PlayOneShot(spawnSound);
    }
}