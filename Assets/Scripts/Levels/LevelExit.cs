using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelExit : MonoBehaviour
{
    private int playersInExit = 0;
    private int totalPlayers;

    private void Start() => totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInExit++;
            Debug.Log($"Игрок(и) достиг(ли) выхода! ({playersInExit}/{totalPlayers})");

            if (playersInExit >= totalPlayers)
                LoadNextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInExit--;
            Debug.Log($"Игрок покинул выход! ({playersInExit}/{totalPlayers})");
        }
    }

    private void LoadNextLevel() => LevelManager.Instance.LoadNextLevel();
}