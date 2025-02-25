using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public float minX, minY, maxX, maxY;

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);
    }
}