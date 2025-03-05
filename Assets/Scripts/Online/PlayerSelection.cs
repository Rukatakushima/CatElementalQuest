using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerSelection : MonoBehaviourPunCallbacks
{
    public TMP_Text playerNickname;

    private Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton, rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    private Player player;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        SetDefaultAvatar();
    }

    private void SetDefaultAvatar()
    {
        playerAvatar.sprite = avatars[0];
        playerProperties["playerAvatar"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void SetPlayerInfo(Player player)
    {
        playerNickname.text = player.NickName;
        this.player = player;
        UpdatePlayerSelection(player);
    }

    public void ApplyLocalChanges()
    {
        bool isLocalPlayer = player == PhotonNetwork.LocalPlayer;

        if (isLocalPlayer)
            backgroundImage.color = highlightColor;

        leftArrowButton.SetActive(isLocalPlayer);
        rightArrowButton.SetActive(isLocalPlayer);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
            playerProperties["playerAvatar"] = avatars.Length - 1;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
            playerProperties["playerAvatar"] = 0;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer,
                                                  ExitGames.Client.Photon.Hashtable dictionaryEntries)
    {
        if (player == targetPlayer)
            UpdatePlayerSelection(targetPlayer);
    }

    private void UpdatePlayerSelection(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
            playerProperties["playerAvatar"] = 0;
    }
}