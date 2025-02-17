using UnityEngine;

public class Twigs : ElementalObject
{
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            currentState = ElementalObjectState.SecondState;
            UpdateElementalObjectSprite();
        }
    }

    private void HandleBurningState(Ability playerAbility)
    {
        if (playerAbility is WaterAbility)
        {
            Debug.Log("Хворост потушен и превратился в пепел!");
            currentState = ElementalObjectState.ThirdState;
            UpdateElementalObjectSprite();
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void HandleAshState(Ability playerAbility)
    {
        if (playerAbility.isAbilityActive && currentState == ElementalObjectState.ThirdState && playerAbility is WindAbility)
        {
            Debug.Log("Пепел раздулся ветром и исчез!");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleAshState(other.gameObject.GetComponent<Ability>());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleAshState(other.gameObject.GetComponent<Ability>());
    }
}