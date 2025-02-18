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
        if (!playerAbility.isAbilityActive)
        {
            Debug.Log("Водяной игрок сгорел!");
            respawnManager.Respawn(playerAbility.gameObject);
            return;
        }

        if (playerAbility is WaterAbility && currentState == ElementalObjectState.FirstState)
        {
            Debug.Log("Водяной игрок потушил лаву");
<<<<<<< HEAD
            UpdateElementalObjectSprite();
            GetComponent<Collider2D>().isTrigger = false;
=======
>>>>>>> parent of 7ca45be (ElementalObject #2)
            currentState = ElementalObjectState.SecondState;
        }

    }
}