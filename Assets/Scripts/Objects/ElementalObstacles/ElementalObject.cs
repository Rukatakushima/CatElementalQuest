using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public abstract class ElementalObject : MonoBehaviourPunCallbacks
{
    public enum ElementalObjectState { FirstState, SecondState, ThirdState }
    protected ElementalObjectState currentState = ElementalObjectState.FirstState;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] stateSprites;
    [SerializeField] protected AudioClip[] stateSounds;
    protected AudioSource audioSource;

    protected UnityEvent<ElementalObjectState> OnStateChanged; // События, что будут происходить при смене состояния в зависимости от этого состояния

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        OnStateChanged = new UnityEvent<ElementalObjectState>();
        // OnStateChanged.AddListener(StateChangeEvent);
        OnStateChanged.AddListener(PlayChangingStateSound);
    }

    protected virtual void Start() => UpdateElementalObjectSprite();

    public abstract void GetInsideElement(Ability playerAbility);

    public abstract void InteractWithElement(Ability playerAbility);

    protected virtual void ChangeState(ElementalObjectState newState) => photonView.RPC("RPC_ChangeState", RpcTarget.All, newState);

    [PunRPC]
    public void RPC_ChangeState(ElementalObjectState newState)
    {
        currentState = newState;
        UpdateElementalObjectSprite();
        OnStateChanged?.Invoke(currentState);
    }

    protected void UpdateElementalObjectSprite() => spriteRenderer.sprite = stateSprites[(int)currentState];

    protected virtual void PlayChangingStateSound(ElementalObjectState newState)
    {
        if (stateSounds[(int)newState - 1] == null) return;

        audioSource.loop = false;
        audioSource.PlayOneShot(stateSounds[(int)newState - 1]);
    }
}