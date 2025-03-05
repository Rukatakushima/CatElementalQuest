using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public abstract class MechanicalObstacles : MonoBehaviourPunCallbacks
{
    [SerializeField] protected AudioClip mechanismSound;
    protected AudioSource audioSource;

    protected UnityEvent OnMechanismActivated = new UnityEvent();

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        OnMechanismActivated.AddListener(PlayMechanismSound);
    }

    protected virtual void PlayMechanismSound()
    {
        if (mechanismSound == null) return;
        audioSource.PlayOneShot(mechanismSound);
    }

    protected virtual void StopMechanismSound() => audioSource.Stop();
}