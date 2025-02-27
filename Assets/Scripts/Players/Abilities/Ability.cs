using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public abstract class Ability : MonoBehaviourPunCallbacks
{
    private Movement playerMovement;

    [SerializeField] private GameObject abilityColliderPrefab;
    [SerializeField] private Vector2 abilityPositionOffset = new Vector2(2f, 0f);
    private GameObject abilityObject;
    private AbilityCollider abilityCollider;
    [SerializeField] private Sprite abilitySprite;

    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    public UnityEvent<bool> OnAbilityActiveChanged;

    private void Awake() => playerMovement = GetComponent<Movement>();

    // private void Start()
    // {
    //     // if (photonView.IsMine)
    //     // {
    //     // abilityObject = PhotonNetwork.Instantiate(abilityColliderPrefab.name, Vector3.zero, Quaternion.identity);
    //     abilityObject = Instantiate(abilityColliderPrefab);
    //     abilityCollider = abilityObject.GetComponent<AbilityCollider>();
    //     abilityCollider.GetComponent<SpriteRenderer>().sprite = abilitySprite;
    //     abilityCollider.abilityType = this;
    //     abilityObject.SetActive(false);
    //     // }
    // }

    private void Update()
    {
        // if (!photonView.IsMine) return;

        // if (Input.GetKeyDown(KeyCode.E))
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.E))
            CreateAbility();

        if (isAbilityActive)
            SetAbilityCooldown();
    }

    private void CreateAbility()
    {
        if (abilityTimer != 0) return;

        photonView.RPC("RPC_CreateAbility", RpcTarget.All);
        // SetAbilityCollider();
        // UseAbility();
    }

    [PunRPC]
    public void RPC_CreateAbility()
    {
        Debug.Log("RPC_CreateAbility вызван");
        if (abilityObject == null)
            CreateNewAbilityObject();

        SetAbilityCollider();
        UseAbility();
    }

    private void CreateNewAbilityObject()
    {
        abilityObject = PhotonNetwork.Instantiate(abilityColliderPrefab.name, Vector3.zero, Quaternion.identity);
        abilityCollider = abilityObject.GetComponent<AbilityCollider>();
        abilityCollider.GetComponent<SpriteRenderer>().sprite = abilitySprite;
        abilityCollider.abilityType = this;
        abilityObject.SetActive(false);
    }

    private void SetAbilityCollider()
    {
        SetColliderActive(true);
        abilityObject.transform.position = playerMovement.isFacingRight ?
            (Vector2)transform.position + abilityPositionOffset : (Vector2)transform.position - abilityPositionOffset;
        abilityCollider.CheckForElementalObjects();
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
            OnAbilityActiveChanged?.Invoke(false);
        }
    }
}