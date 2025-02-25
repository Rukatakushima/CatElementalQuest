using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    private Movement playerMovement;

    [SerializeField] private GameObject abilityColliderPrefab;
    [SerializeField] private Vector2 abilityPositionOffset = new Vector2(2f, 0f);
    private GameObject abilityCollider;

    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    public UnityEvent<bool> OnAbilityActiveChanged;

    [SerializeField] private Vector2 startAbilityPosition;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        abilityCollider = Instantiate(abilityColliderPrefab);
    }

    private void Start()
    {
        abilityCollider.GetComponent<AbilityCollider>().abilityType = this;
        // abilityCollider.SetActive(false);
        abilityCollider.transform.position = startAbilityPosition;
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
        SetAbilityCollider();
        UseAbility();
    }

    private void SetAbilityCollider()
    {
        SetColliderActive(true);

        abilityCollider.transform.position = playerMovement.isFacingRight ?
            (Vector2)transform.position + abilityPositionOffset : (Vector2)transform.position - abilityPositionOffset;
    }

    private void SetColliderActive(bool isActive)
    {
        isAbilityActive = isActive;
        abilityCollider.SetActive(isActive);
    }

    public abstract void UseAbility();

    private void SetAbilityCooldown()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= abilityCooldown)
        {
            // isAbilityActive = false;
            // SetColliderActive(false);
            abilityCollider.transform.position = startAbilityPosition;
            abilityTimer = 0f;
            OnAbilityActiveChanged?.Invoke(false);
        }
    }
}