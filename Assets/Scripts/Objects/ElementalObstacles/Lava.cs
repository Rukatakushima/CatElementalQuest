
using UnityEngine;

public class Lava : DangerousElementalObject
{
    public override void GetInsideElement(Ability playerAbility)
    {
        if (playerAbility is FireAbility || currentState == ElementalObjectState.SecondState)
            PlayWalkingSound();
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

        ChangeState(ElementalObjectState.SecondState);
        SetColliderTrigger(false);
    }

    public void BurnPlayer(GameObject player) => RespawnPlayer(player);
}