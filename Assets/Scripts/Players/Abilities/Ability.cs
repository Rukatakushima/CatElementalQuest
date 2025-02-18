using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAbilityActive = true;
            UseAbility();
        }

        if (isAbilityActive)
            SetAbilityCooldown();
    }

    private void SetAbilityCooldown()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= abilityCooldown)
        {
            isAbilityActive = false;
            abilityTimer = 0f;
        }
    }

    public abstract void UseAbility();

    //для блуждения по воде и лаве
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     ElementalObject elementalObject = other.gameObject.GetComponent<ElementalObject>();
    //     if (elementalObject != null)
    //     {
    //         elementalObject.HandleInstantInteraction(this);
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     if (!isAbilityActive) return;

    //     ElementalObject elementalObject = other.gameObject.GetComponent<ElementalObject>();
    //     if (elementalObject != null)
    //     {
    //         elementalObject.HandleContinuousInteraction(this);
    //     }
    // }
}