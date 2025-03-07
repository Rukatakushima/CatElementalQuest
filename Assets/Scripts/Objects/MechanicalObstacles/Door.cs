using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Door : MechanicalObstacles
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 openPosition;
    private Vector2 closedPosition;
    private bool isOpen = false;
    private bool isMechanismSoundPlaying = false;

    private void Start() => closedPosition = transform.position;

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
            OnMechanismActivated?.Invoke();
            yield return null;
        }

        transform.position = targetPosition;
        StopMechanismSound();
    }

    protected override void PlayMechanismSound()
    {
        if (mechanismSound == null || isMechanismSoundPlaying) return;

        isMechanismSoundPlaying = true;
        audioSource.PlayOneShot(mechanismSound);
    }

    protected override void StopMechanismSound()
    {
        audioSource.Stop();
        isMechanismSoundPlaying = false;
    }
}