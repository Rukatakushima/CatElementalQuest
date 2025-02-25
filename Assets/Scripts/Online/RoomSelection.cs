using UnityEngine;
using TMPro;

public class RoomSelection : MonoBehaviour
{
    public TMP_Text roomName;

    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindAnyObjectByType<MenuManager>();
    }

    public void SetRoomName(string roomName)
    {
        this.roomName.text = "Join room: " + roomName;
    }

    public void OnClickJoinRoom()
    {
        menuManager.JoinRoom(roomName.text);
    }
}
