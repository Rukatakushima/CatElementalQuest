using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class MenuManager : MonoBehaviourPunCallbacks
{

    [Header("Lobby Settings")]
    public GameObject lobbyPanel;

    [SerializeField] private string defaultNickname = "Purrlayer";
    public TMP_InputField nicknameInput;
    public TMP_InputField createRoomInput;
    public TMP_InputField joinRoomInput;

    public Transform roomSelectionParent;
    public RoomSelection roomSelectionPrefab;
    private List<RoomSelection> roomSelectionList = new();

    [SerializeField] private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

    [Header("Room Settings")]
    public GameObject roomPanel;
    public TMP_Text roomName;

    public Transform playerSelectionParent; // == roomPanel
    public PlayerSelection playerSelectionPrefab;
    public List<PlayerSelection> playerSelectionsList = new();

    public GameObject playButton;

    [Header("Game Settings")]
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private int playersCountToStart = 1;

    private void Awake() => TogglePanels(false);

    private void Start() => PhotonNetwork.JoinLobby();

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= playersCountToStart)
            playButton.SetActive(true);
        else
            playButton.SetActive(false);
    }

    public void OnClickCreateRoom()
    {
        if (createRoomInput.text.Length >= 1)
        {
            SaveNickName();
            PhotonNetwork.CreateRoom(createRoomInput.text, SetRoomOptions());
        }
    }

    public void JoinRoom(string joinRoomName)
    {
        SaveNickName();
        PhotonNetwork.JoinRoom(joinRoomName);
    }

    private void SaveNickName()
    {
        if (nicknameInput.text.Length >= 1)
            PhotonNetwork.NickName = nicknameInput.text;
        else
            PhotonNetwork.NickName = defaultNickname;
    }

    private RoomOptions SetRoomOptions()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;

        roomOptions.BroadcastPropsChangeToAll = true;

        return roomOptions;
    }

    public void OnConnectedToServer() => PhotonNetwork.JoinLobby();

    public override void OnJoinedRoom()
    {
        TogglePanels(true);
        roomName.text = "Room Name:<br>" + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayersList();
    }

    private void TogglePanels(bool isChangingLobbyToRoom)
    {
        lobbyPanel.SetActive(!isChangingLobbyToRoom);
        roomPanel.SetActive(isChangingLobbyToRoom);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> rooms)
    {
        DestroyAllRoomsInList();
        CreateRoomsList(rooms);
    }

    private void DestroyAllRoomsInList()
    {
        foreach (RoomSelection item in roomSelectionList)
        {
            Destroy(item.gameObject);
        }
        roomSelectionList.Clear();
    }

    private void CreateRoomsList(List<RoomInfo> rooms)
    {
        foreach (RoomInfo room in rooms)
        {
            RoomSelection newRoom = Instantiate(roomSelectionPrefab, roomSelectionParent);
            newRoom.SetRoomName(room.Name);
            roomSelectionList.Add(newRoom);
        }
    }

    public void OnClickLeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnLeftRoom() => TogglePanels(false);

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnPlayerEnteredRoom(Player newPlayer) => UpdatePlayersList();

    public override void OnPlayerLeftRoom(Player otherPlayer) => UpdatePlayersList();

    private void UpdatePlayersList()
    {
        ClearPlayersList();
        if (PhotonNetwork.CurrentRoom == null) return;

        CreatePlayerList();
    }

    private void ClearPlayersList()
    {
        foreach (PlayerSelection item in playerSelectionsList)
        {
            Destroy(item.gameObject);
        }
        playerSelectionsList.Clear();
    }

    private void CreatePlayerList()
    {
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerSelection newPlayerSelection = Instantiate(playerSelectionPrefab, playerSelectionParent); // == roomPanel.transform
            newPlayerSelection.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
                newPlayerSelection.ApplyLocalChanges();

            playerSelectionsList.Add(newPlayerSelection);
        }
    }

    public void OnClickPlayButton() => PhotonNetwork.LoadLevel("Game");
}
