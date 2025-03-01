using UnityEngine;
using Photon.Pun;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PhotonView))]
public class Door : MonoBehaviourPunCallbacks
{
    [SerializeField] private float openHeight = 5f;
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 openPosition;
    private Vector2 closedPosition;
    private bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector2.up * openHeight;
    }

    public void ChangeDoorPosition() => photonView.RPC("RPC_SetDoorState", RpcTarget.All, !isOpen);

    [PunRPC]
    public void RPC_SetDoorState(bool isOpen)
    {
        this.isOpen = isOpen;
        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? openPosition : closedPosition));
    }

    private IEnumerator MoveDoor(Vector2 targetPosition)
    {
        Debug.Log("Дверь двигается, текущая позиция: " + (Vector2)transform.position + "\n цель: " + targetPosition + "\n isOpen = " + isOpen);
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}