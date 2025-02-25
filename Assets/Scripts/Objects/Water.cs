using UnityEngine;

public class Water : DangerousElementalObject
{
    public override void GetInsideElement(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через лед.");
            SlipOnIce(playerAbility.gameObject);
            // if (playerAbility is FireAbility)
            // {
            //     Debug.Log("Огненный игрок безопасно проходит через воду.");
            //     InteractWithElement(playerAbility);
            // }
        }
        else if (playerAbility is WaterAbility || playerAbility is IceAbility)
        {
            Debug.Log("Водный или ледяной игрок безопасно проходит через воду.");
        }
        else
            DrownPlayer(playerAbility.gameObject);
    }

    public override void InteractWithElement(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive) return;

        if (playerAbility is IceAbility)
            FreezeWater();
        else if (playerAbility is FireAbility)
            DefrostWater();
    }

    private void FreezeWater()
    {
        if (currentState != ElementalObjectState.FirstState) return;

        Debug.Log("Вода заморожена"); ;
        ChangeState(ElementalObjectState.SecondState);
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void DefrostWater()
    {
        if (currentState != ElementalObjectState.SecondState) return;

        Debug.Log("Вода разморожена");
        ChangeState(ElementalObjectState.FirstState);
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void DrownPlayer(GameObject player)
    {
        Debug.Log("Игрок утонул!");
        respawnManager.Respawn(player);
    }

    private void SlipOnIce(GameObject player)
    {
        Debug.Log("Игрок скользит по льду!");
    }

    protected override void StateChangeEvent(ElementalObjectState newState) => Debug.Log($"Состояние воды изменено на: {newState}");
}