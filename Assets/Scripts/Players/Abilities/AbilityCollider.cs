using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AbilityCollider : MonoBehaviour
{
    // private Transform player;
    public Ability abilityType;

    // private Movement playerMovement;
    // [SerializeField] private Vector2 rightPosition, leftPosition;

    // private bool isPositionFixed { get; set; } = false;
    // private Vector2 fixedPosition { get; set; }

    // private Collider2D abilityCollider;
    // private SpriteRenderer abilitySpriteRenderer;

    // private void Awake()
    // {
    //     // abilityCollider = GetComponent<Collider2D>();
    //     // abilitySpriteRenderer = GetComponent<SpriteRenderer>();

    //     // player = transform.parent;
    //     // playerAbility = player.GetComponentInParent<Ability>();
    //     // playerMovement = player.GetComponentInParent<Movement>();

    //     playerAbility.OnAbilityActiveChanged.AddListener(SetColliderActive);
    // }

    // private void Start() => SetColliderActive(false);

    // private void Update() => UpdatePosition();

    // private void UpdatePosition()
    // {
    //     if (!isPositionFixed)
    //     {
    //         Vector2 newPosition = playerMovement.isFacingRight ? rightPosition : leftPosition;

    //         if ((Vector2)transform.position != newPosition)
    //             transform.localPosition = newPosition;
    //     }
    //     else
    //         transform.position = fixedPosition;
    // }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        ElementalObject elementalObject = other.GetComponent<ElementalObject>();
        Debug.Log("Ability triggered. elementalObject: ");
        Debug.Log(elementalObject);

        if (elementalObject != null && abilityType.isAbilityActive)
            elementalObject.HandleContinuousInteraction(abilityType);
    }

    // private void SetColliderActive(bool isActive)
    // {
    //     this.gameObject.SetActive(isActive);
    //     // abilitySpriteRenderer.enabled = isActive;
    //     // abilityCollider.enabled = isActive;

    //     // isPositionFixed = isActive;
    //     // if (isPositionFixed)
    //     //     fixedPosition = transform.position;
    // }
}