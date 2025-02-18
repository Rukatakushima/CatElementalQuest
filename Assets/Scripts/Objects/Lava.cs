
using UnityEngine;

public class Lava : DangerousElementalObject
{
    public override void HandleInstantInteraction(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через потушенную лаву.");
        }
        else if (playerAbility is FireAbility)
        {
            Debug.Log("Огненный игрок безопасно проходит через лаву.");
        }
        else
        {
            Debug.Log("Игрок сгорел!");
            respawnManager.Respawn(playerAbility.gameObject);
        }
    }

    public override void HandleContinuousInteraction(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive) return;

        if (playerAbility is WaterAbility && currentState == ElementalObjectState.FirstState)
        {
            Debug.Log("Водяной игрок потушил лаву");
            ChangeState(ElementalObjectState.SecondState);
        }
    }

    protected override void HandleStateChange(ElementalObjectState newState)
    {
        Debug.Log($"Состояние лавы изменено на: {newState}");
    }
}
/*

public class Lava : DangerousElementalObject
{
    public override void HandleInstantInteraction(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через потушенную лаву.");
        }
        else if (playerAbility is FireAbility)
        {
            Debug.Log("Огненный игрок безопасно проходит через огонь.");
        }
        else if (playerAbility is WaterAbility)
        {
            HandleContinuousInteraction(playerAbility);
            Debug.Log("Водяной игрок проходит через огонь.");
        }
        else
        {
            Debug.Log("Игрок сгорел!");
            respawnManager.Respawn(playerAbility.gameObject);
        }
    }

    public override void HandleContinuousInteraction(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive || !(playerAbility is FireAbility))
        {
            Debug.Log("Игрок (водяной) сгорел!");
            respawnManager.Respawn(playerAbility.gameObject);
            return;
        }
        // if (!playerAbility.isAbilityActive) return;

        if (playerAbility is WaterAbility && currentState == ElementalObjectState.FirstState)
        {
            Debug.Log("Водяной игрок потушил лаву");
            // currentState = ElementalObjectState.SecondState;
            ChangeState(ElementalObjectState.SecondState);
        }
    }
}
*/