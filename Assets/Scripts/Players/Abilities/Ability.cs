using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public abstract class Ability : MonoBehaviourPunCallbacks
{
    private Movement playerMovement;

    [SerializeField] private GameObject abilityColliderPrefab;
    private GameObject abilityObject;
    private AbilityCollider abilityCollider;

    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    [SerializeField] private Vector2 abilityPositionOffset = new Vector2(2f, 0f);
    [SerializeField] private Sprite abilitySprite;
    [SerializeField] private AudioClip abilitySound;
    private AudioSource audioSource;

    private UnityEvent OnAbilityActive = new();

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        CreateNewAbilityObject();

        OnAbilityActive.AddListener(PlayAbilitySound);
    }

    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.E))
            SpawnAbility();

        if (isAbilityActive)
            SetAbilityCooldown();
    }

    private void SpawnAbility()
    {
        if (abilityTimer != 0) return;
        photonView.RPC("RPC_SpawnAbility", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_SpawnAbility()
    {
        SetAbilityCollider();
        UseAbility();
        OnAbilityActive?.Invoke();
    }

    private void CreateNewAbilityObject()
    {
        if (abilityObject != null) return;

        abilityObject = Instantiate(abilityColliderPrefab);
        abilityObject.GetComponent<SpriteRenderer>().sprite = abilitySprite;
        abilityObject.name = ToString();

        abilityCollider = abilityObject.GetComponent<AbilityCollider>();
        abilityCollider.abilityType = this;
        abilityObject.SetActive(false);
    }

    private void SetAbilityCollider()
    {
        SetColliderActive(true);
        abilityObject.transform.position = playerMovement.IsFacingRight ?
            (Vector2)transform.position + abilityPositionOffset : (Vector2)transform.position - abilityPositionOffset;
    }

    private void SetColliderActive(bool isActive)
    {
        isAbilityActive = isActive;
        abilityObject.SetActive(isActive);
    }

    public abstract void UseAbility();

    private void SetAbilityCooldown()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= abilityCooldown)
        {
            SetColliderActive(false);
            abilityTimer = 0f;
        }
    }

    private void PlayAbilitySound()
    {
        if (abilitySound != null && audioSource != null)
            audioSource.PlayOneShot(abilitySound);
    }
}