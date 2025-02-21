using UnityEngine;

public class Stone : ElementalObject
{
    protected override void Start()
    {
        base.Start();
        UpdateElementalObjectSprite();
    }

    public override void HandleInstantInteraction(Ability playerAbility) { }

    public override void HandleContinuousInteraction(Ability playerAbility)
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
        // ChangeState(ElementalObjectState.ThirdState);
        gameObject.SetActive(false);
        // UpdateElementalObjectSprite();
    }

    protected override void HandleStateChangeEvent(ElementalObjectState newState) => Debug.Log($"Состояние хвороста изменено на: {newState}");
}