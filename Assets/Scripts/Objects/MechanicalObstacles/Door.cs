using UnityEngine;
using Photon.Pun;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviourPunCallbacks
{
    [SerializeField] private float openHeight = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 openPosition;
    private Vector2 closedPosition;
    private bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector2.up * openHeight;
    }

    // For Lever Event
    public void ChangeDoorPosition()
    {
        if (isOpen)
            HandleLeverStateChanged(!isOpen);
        else
            HandleLeverStateChanged(isOpen);
    }

    private void HandleLeverStateChanged(bool isActivated) => photonView.RPC("RPC_SetDoorState", RpcTarget.All, isActivated);

    [PunRPC]
    public void RPC_SetDoorState(bool isOpen)
    {
        this.isOpen = isOpen;
        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? openPosition : closedPosition));
    }

    private IEnumerator MoveDoor(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}