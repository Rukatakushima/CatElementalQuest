using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class Lever : MonoBehaviourPunCallbacks
{
    [SerializeField] private KeyCode interactKey = KeyCode.Q;
    private bool isActivated = false;

    public static UnityEvent OnLeverActivated;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(interactKey))
            photonView.RPC("RPC_ActivateLever", RpcTarget.All);
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