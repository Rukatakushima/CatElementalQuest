using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    public bool isAbilityActive { get; private set; } = false;
    [SerializeField] private float abilityCooldown = 2f;
    private float abilityTimer = 0f;

    public UnityEvent<bool> OnAbilityActiveChanged;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAbilityActive = true;
            UseAbility();
            OnAbilityActiveChanged?.Invoke(true);
        }

        if (isAbilityActive)
            SetAbilityCooldown();
    }

    public abstract void UseAbility();

    private void SetAbilityCooldown()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= abilityCooldown)
        {
            isAbilityActive = false;
            abilityTimer = 0f;
            OnAbilityActiveChanged?.Invoke(false);
        }
    }
}