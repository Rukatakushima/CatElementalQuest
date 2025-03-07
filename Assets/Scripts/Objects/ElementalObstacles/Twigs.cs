using UnityEngine;

public class Twigs : ElementalObject
{
    public override void InteractWithElement(Ability playerAbility)
    {
        switch (currentState)
        {
            case ElementalObjectState.FirstState:
                HandleNormalState(playerAbility);
                break;
            case ElementalObjectState.SecondState:
                HandleBurningState(playerAbility);
                break;
            default:
                break;
        }
    }

    private void HandleNormalState(Ability playerAbility)
    {
        if (playerAbility is FireAbility)
        {
            ChangeState(ElementalObjectState.SecondState);
            UpdateElementalObjectSprite();
        }
    }

    private void HandleBurningState(Ability playerAbility)
    {
        if (playerAbility is WaterAbility)
        {
            ChangeState(ElementalObjectState.ThirdState);
            UpdateElementalObjectSprite();
            SetColliderTrigger(true);
        }
    }

    private void HandleAshState()
    {
        if (currentState == ElementalObjectState.ThirdState)
            gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility is WindAbility && playerAbility.isAbilityActive)
            HandleAshState();
    }
}