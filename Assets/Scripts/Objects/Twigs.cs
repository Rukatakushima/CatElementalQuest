using UnityEngine;

public class Twigs : ElementalObject
{
    protected override void Start()
    {
        base.Start();
        UpdateElementalObjectSprite();
    }

    public override void HandleInstantInteraction(Ability playerAbility) { }

    public override void HandleContinuousInteraction(Ability playerAbility)
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
            // currentState = ElementalObjectState.SecondState;
            ChangeState(ElementalObjectState.SecondState);
            UpdateElementalObjectSprite();
        }
    }

    private void HandleBurningState(Ability playerAbility)
    {
        if (playerAbility is WaterAbility)
        {
            Debug.Log("Хворост потушен и превратился в пепел!");
            // currentState = ElementalObjectState.ThirdState;
            ChangeState(ElementalObjectState.ThirdState);
            UpdateElementalObjectSprite();
            GetComponent<Collider2D>().isTrigger = true;
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

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     HandleAshState(other.gameObject.GetComponent<Ability>());
    // }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Ability playerAbility = other.gameObject.GetComponent<Ability>();

    //     if (playerAbility)
    //         HandleAshState(playerAbility);
    // }

    // protected override void HandleTriggerInteraction(Collider2D other)
    // {
    //     HandleAshState(other.gameObject.GetComponent<Ability>());
    // }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        // HandleTriggerInteraction(other);
        HandleAshState(other.gameObject.GetComponent<Ability>());
    }
    
    protected override void HandleStateChangeEvent(ElementalObjectState newState)
    {
        Debug.Log($"Состояние хвороста изменено на: {newState}");
    }
}