using UnityEngine;

public class Twigs : ElementalObject
{
    public override void GetInsideElement(Ability playerAbility) { }

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
            Debug.Log("Хворост загорелся!");
            ChangeState(ElementalObjectState.SecondState);
            UpdateElementalObjectSprite();
        }
    }

    private void HandleBurningState(Ability playerAbility)
    {
        if (playerAbility is WaterAbility)
        {
            Debug.Log("Хворост потушен и превратился в пепел!");
            ChangeState(ElementalObjectState.ThirdState);
            UpdateElementalObjectSprite();
            SetColliderTrigger(true);
            // GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void HandleAshState(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.ThirdState &&
            playerAbility is WindAbility &&
            playerAbility.isAbilityActive)
        {
            Debug.Log("Пепел раздулся ветром и исчез!");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other) => HandleAshState(other.gameObject.GetComponent<Ability>());
}