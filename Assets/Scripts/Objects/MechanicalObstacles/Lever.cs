using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PhotonView))]
public class Lever : MonoBehaviourPunCallbacks
{
    [SerializeField] private KeyCode interactKey = KeyCode.Q;
    private bool isActivated = false;

    public UnityEvent OnLeverActivated = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                Debug.Log("Нажмите " + interactKey + ", чтобы активировать рычаг");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                Debug.Log("Вы вышли из зоны взаимодействия");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();

            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    Debug.Log("photonView = " + photonView);
                    photonView.RPC("RPC_SwitchLever", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    public void RPC_SwitchLever()
    {
        SwitchLever();

        if (isActivated)
            OnLeverActivated?.Invoke();
    }

    // private void ActivateLever()
    // {
    //     isActivated = true;
    //     RotateLeverHandle();
    //     OnLeverActivated?.Invoke();
    // }

    // private void DeactivateLever()
    // {
    //     isActivated = false;
    //     RotateLeverHandle();
    // }

    private void SwitchLever()
    {
        isActivated = !isActivated;
        RotateLeverHandle();
    }

    private void RotateLeverHandle()
    {
        Vector2 leverScale = transform.localScale;
        leverScale.x *= -1;
        transform.localScale = leverScale;
    }
}