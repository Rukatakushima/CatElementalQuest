using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

    [Header("Lobby Settings")]
    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private string defaultNickname = "Purrlayer";
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private TMP_InputField createRoomInput;
    // public TMP_InputField joinRoomInput;

    [SerializeField] private Transform roomSelectionParent;
    [SerializeField] private RoomSelection roomSelectionPrefab;
    private List<RoomSelection> roomSelectionList = new();

    [Header("Room Settings")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private GameObject playButton;

    private Transform playerSelectionParent; // == roomPanel
    [SerializeField] private PlayerSelection playerSelectionPrefab;
    [SerializeField] private List<PlayerSelection> playerSelectionsList = new();

    [Header("Game Settings")]
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private int playersCountToStart = 1;
    [SerializeField] private string gameLevelName = "Level1";

    private void Awake() => playerSelectionParent = roomPanel.transform;

    private void Start()
    {
        TogglePanels(false);
        PhotonNetwork.JoinLobby();
    }

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
            PlayerSelection newPlayerSelection = Instantiate(playerSelectionPrefab, playerSelectionParent);
            newPlayerSelection.SetPlayerInfo(player.Value);
            newPlayerSelection.ApplyLocalChanges();

            playerSelectionsList.Add(newPlayerSelection);
        }
    }

    public void OnClickPlayButton() => PhotonNetwork.LoadLevel(gameLevelName);
}
