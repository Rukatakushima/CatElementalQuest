using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Spinner : MonoBehaviour
{
    [SerializeField] private Vector3 spinAxis = new Vector3(0, 0, 1);
    [SerializeField] private float spinSpeed = 50f;

    private void Update() => Spin();

    private void Spin() => transform.Rotate(spinAxis * spinSpeed * Time.deltaTime);
}