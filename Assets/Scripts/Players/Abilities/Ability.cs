using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using System.Collections;

public abstract class Ability : MonoBehaviourPunCallbacks
{
    private Movement playerMovement;

    [SerializeField] private GameObject abilityColliderPrefab;
    protected GameObject abilityObject;
    private AbilityCollider abilityCollider;

    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    // private float abilityTimer = 0f;
    [SerializeField] protected bool shouldDeactiveCollider = true;

    [SerializeField] private Vector2 abilityPositionOffset = new Vector2(2f, 0f);
    [SerializeField] private Sprite abilitySprite;
    [SerializeField] private AudioClip abilitySound;
    private AudioSource audioSource;

    private UnityEvent OnAbilityActive = new();

    protected virtual void Awake()
    {
        playerMovement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        CreateNewAbilityObject();

        OnAbilityActive.AddListener(PlayAbilitySound);
        OnAbilityActive.AddListener(abilityCollider.CheckForElementalObjects);
    }

    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.E))
            SpawnAbility();

        // if (isAbilityActive)
        //     SetAbilityCooldown();
    }

    private void SpawnAbility()
    {
        if (isAbilityActive) return;
        photonView.RPC("RPC_SpawnAbility", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_SpawnAbility()
    {
        SetAbilityCollider();
        UseAbility();
        StartCoroutine(StartAbilityCooldown());
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

    private IEnumerator StartAbilityCooldown()
    {
        isAbilityActive = true;
        yield return new WaitForSeconds(abilityCooldown);

        if (shouldDeactiveCollider)
            SetColliderActive(false);
        isAbilityActive = false;
    }

    // private void SetAbilityCooldown()
    // {
    //     abilityTimer += Time.deltaTime;
    //     if (abilityTimer >= abilityCooldown)
    //     {
    //         if (shouldDeactiveCollider)
    //             SetColliderActive(false);
    //         abilityTimer = 0f;
    //     }
    // }

    private void PlayAbilitySound()
    {
        if (abilitySound != null && audioSource != null)
            audioSource.PlayOneShot(abilitySound);
    }
}