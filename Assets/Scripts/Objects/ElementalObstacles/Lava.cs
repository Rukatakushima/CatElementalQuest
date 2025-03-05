
using UnityEngine;

public class Lava : DangerousElementalObject
{
    public override void GetInsideElement(Ability playerAbility)
    {
        if (playerAbility is FireAbility || currentState == ElementalObjectState.SecondState)
        {
            PlayWalkingSound();
            Debug.Log("Любой игрок безопасно проходит через потушенную лаву.");
        }
        else
            BurnPlayer(playerAbility.gameObject);
    }

    public override void InteractWithElement(Ability playerAbility)
    {
        if (!playerAbility.isAbilityActive) return;

        if (playerAbility is WaterAbility)
            ExtinguishFire();
    }

    public void ExtinguishFire()
    {
        if (currentState != ElementalObjectState.FirstState) return;

        Debug.Log("Водяной игрок потушил лаву");
        ChangeState(ElementalObjectState.SecondState);
        GetComponent<Collider2D>().isTrigger = false;
    }

    public void BurnPlayer(GameObject player)
    {
        Debug.Log("Игрок сгорел!");
        respawnManager.Respawn(player);
    }
}