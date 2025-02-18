using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected RespawnManager respawnManager;

    protected override void Start()
    {
        base.Start();
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
        {
            HandleInstantInteraction(playerAbility);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null && playerAbility.isAbilityActive)
            HandleContinuousInteraction(playerAbility);

        // if (!isAbilityActive) return;

        // ElementalObject elementalObject = other.gameObject.GetComponent<ElementalObject>();
        // if (elementalObject != null)
        // {
        //     elementalObject.HandleContinuousInteraction(this);
        // }
    }
}