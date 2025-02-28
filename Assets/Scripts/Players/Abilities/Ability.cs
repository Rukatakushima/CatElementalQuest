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

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        CreateNewAbilityObject();
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
    }

    private void CreateNewAbilityObject()
    {
        if (abilityObject != null) return;

        // abilityObject = PhotonNetwork.Instantiate(abilityColliderPrefab.name, Vector3.zero, Quaternion.identity);
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
        abilityObject.transform.position = playerMovement.isFacingRight ?
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
            OnAbilityActiveChanged?.Invoke(false);
        }
    }
}