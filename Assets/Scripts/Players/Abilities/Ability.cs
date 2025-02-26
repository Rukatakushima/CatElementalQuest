using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    private Movement playerMovement;

    [SerializeField] private GameObject abilityColliderPrefab;
    [SerializeField] private Vector2 abilityPositionOffset = new Vector2(2f, 0f);
    private GameObject abilityObject;
    private AbilityCollider abilityCollider;

    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    public UnityEvent<bool> OnAbilityActiveChanged;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        abilityObject = Instantiate(abilityColliderPrefab);
        abilityCollider = abilityObject.GetComponent<AbilityCollider>();
    }

    private void Start()
    {
        abilityCollider.abilityType = this;
        abilityObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CreateAbility();

        if (isAbilityActive)
            SetAbilityCooldown();
    }

    private void CreateAbility()
    {
        if (abilityTimer != 0) return;

        SetAbilityCollider();
        UseAbility();
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