using UnityEngine;

public class Water : DangerousElementalObject
{
    [SerializeField] private float slideForce = 25f;

    public override void GetInsideElement(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            PlayWalkingSound();
            if (playerAbility is IceAbility) return;
            SlipOnIce(playerAbility.gameObject);
        }
        else if (playerAbility is WaterAbility || playerAbility is IceAbility)
            PlayWalkingSound();
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

        ChangeState(ElementalObjectState.SecondState);
        SetColliderTrigger(false);
    }

    private void DefrostWater()
    {
        if (currentState != ElementalObjectState.SecondState) return;

        ChangeState(ElementalObjectState.FirstState);
        SetColliderTrigger(true);
    }

    private void DrownPlayer(GameObject player) => RespawnPlayer(player);

    private void SlipOnIce(GameObject player)
    {
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement == null) return;

        playerMovement.StartSliding(slideForce);
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != ElementalObjectState.SecondState) return;

        Movement playerMovement = collision.gameObject.GetComponent<Movement>();
        if (playerMovement == null) return;

        playerMovement.StopSliding();
    }
}