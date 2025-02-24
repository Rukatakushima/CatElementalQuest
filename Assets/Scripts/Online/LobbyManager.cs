using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public GameObject roomPanel;
    public TMP_Text roomName;

    public List<PlayerSelection> playerSelectionsList = new();
    public PlayerSelection playerSelectionPrefab;
    public Transform playerSelectionParent;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnConnectedToServer()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayersList();
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayersList();
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayersList();
    }

    private void UpdatePlayersList()
    {
        ClearPlayersList();
        if (PhotonNetwork.CurrentRoom == null) return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerSelection newPlayerSelection = Instantiate(playerSelectionPrefab, playerSelectionParent);
            playerSelectionsList.Add(newPlayerSelection);
        }
    }

    private void ClearPlayersList()
    {
        foreach (PlayerSelection item in playerSelectionsList)
        {
            Destroy(item.gameObject);
        }
        playerSelectionsList.Clear();
    }
}
