using UnityEngine;

public class Stone : ElementalObject
{
    public override void GetInsideElement(Ability playerAbility) { }

    public override void InteractWithElement(Ability playerAbility)
    {
        if (playerAbility is EarthAbility)
        {
            switch (currentState)
            {
                case ElementalObjectState.FirstState:
                    HandleNormalState();
                    break;
                case ElementalObjectState.SecondState:
                    HandleDamagedState();
                    break;
                default:
                    break;
            }
        }
    }

    private void HandleNormalState()
    {
        Debug.Log("Каменная стена разрушается!");
        ChangeState(ElementalObjectState.SecondState);
        UpdateElementalObjectSprite();
    }

    private void HandleDamagedState()
    {
        Debug.Log("Каменная стена полностью разрушена!");
        gameObject.SetActive(false);
    }
}