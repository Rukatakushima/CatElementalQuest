using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected RespawnManager respawnManager;

    protected override void Start()
    {
        base.Start();
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("DEO collisioned");
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);

        AbilityCollider abilityCollider = other.gameObject.GetComponent<AbilityCollider>();
        if (abilityCollider != null && abilityCollider.abilityType.isAbilityActive)
            InteractWithElement(abilityCollider.abilityType);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("DEO Triggered");
        Ability playerAbility = collision.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
        {
            GetInsideElement(playerAbility);
            Debug.Log("GetInsideElement");
        }

        AbilityCollider abilityCollider = collision.gameObject.GetComponent<AbilityCollider>();
        if (abilityCollider != null && abilityCollider.abilityType.isAbilityActive)
        {
            InteractWithElement(abilityCollider.abilityType);
            Debug.Log("InteractWithElement");
        }

    }
}