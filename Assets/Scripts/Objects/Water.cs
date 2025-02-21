using UnityEngine;

public class Water : DangerousElementalObject
{
    public override void HandleInstantInteraction(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через лед.");
            if (playerAbility is FireAbility)
            {
                Debug.Log("Оненный игрок безопасно проходит через воду.");
                HandleContinuousInteraction(playerAbility);
            }
        }
        else if (playerAbility is WaterAbility || playerAbility is IceAbility)
        {
            Debug.Log("Водный или ледяной игрок безопасно проходит через воду.");
        }
        // else if (playerAbility is WaterAbility)
        // {
        //     Debug.Log("Водный игрок безопасно проходит через воду.");
        // }
        // else if (playerAbility is IceAbility)
        // {
        //     HandleContinuousInteraction(playerAbility);
        //     Debug.Log("Ледяной игрок безопасно проходит через воду.");
        // }
        else
        {
            Debug.Log("Игрок утонул!");
            respawnManager.Respawn(playerAbility.gameObject);
        }
    }

    public override void HandleContinuousInteraction(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive) return;

        if (playerAbility is IceAbility && currentState == ElementalObjectState.FirstState)
        {
            Debug.Log("Леденной игрок заморозил воду");
            // currentState = ElementalObjectState.SecondState;
            ChangeState(ElementalObjectState.SecondState);
            GetComponent<Collider2D>().isTrigger = false;
        }
        else if (playerAbility is FireAbility && currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Огненный игрок разморозил воду");
            // currentState = ElementalObjectState.FirstState;
            ChangeState(ElementalObjectState.FirstState);
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    protected override void HandleStateChangeEvent(ElementalObjectState newState)
    {
        Debug.Log($"Состояние воды изменено на: {newState}");
    }
}