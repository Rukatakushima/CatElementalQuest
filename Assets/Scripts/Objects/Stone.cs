using UnityEngine;

public class Stone : ElementalObject
{
    public override void HandleInstantInteraction(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState) return;
        
        HandleContinuousInteraction(playerAbility);
    }

    public override void HandleContinuousInteraction(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive) return;

        if (playerAbility is EarthAbility && currentState == ElementalObjectState.FirstState)
        {
            Debug.Log("Каменный игрок разбил камень");
            currentState = ElementalObjectState.SecondState;
            UpdateElementalObjectSprite();
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}