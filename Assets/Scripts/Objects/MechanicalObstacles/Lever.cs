using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Lever : MonoBehaviourPunCallbacks
{
    [SerializeField] private KeyCode interactKey = KeyCode.Q;
    private bool isActivated = false;

    public static UnityEvent OnLeverActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                Debug.Log("Нажмите E, чтобы активировать рычаг");
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
                    photonView.RPC("RPC_ActivateLever", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void RPC_ActivateLever()
    {
        if (isActivated) return;

        isActivated = true;
        RotateLeverHandle();
        OnLeverActivated?.Invoke();
    }

    private void RotateLeverHandle()
    {
        Vector2 leverScale = transform.localScale;
        leverScale.x *= -1;
        transform.localScale = leverScale;
    }
}