using UnityEngine;

public class Lava : ElementalObject
{
    [SerializeField] protected RespawnManager respawnManager;

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
            Debug.Log("Водяной игрок безопасно проходит через воду.");
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
            currentState = ElementalObjectState.SecondState;
            UpdateElementalObjectSprite();
        }

    }
}