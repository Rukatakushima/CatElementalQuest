using UnityEngine;
using TMPro;

public class RoomSelection : MonoBehaviour
{
    public TMP_Text roomName;
    [SerializeField] private string joinRoomTitle = "Join room: ";

    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindAnyObjectByType<MenuManager>();
    }

    public void SetRoomName(string roomName)
    {
        this.roomName.text = joinRoomTitle + roomName;
    }

    public void OnClickJoinRoom()
    {
        string roomNameOnly = roomName.text.Replace(joinRoomTitle, "");
        menuManager.JoinRoom(roomNameOnly);
        // menuManager.JoinRoom(roomName.text);
    }
}