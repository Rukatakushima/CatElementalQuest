using UnityEngine;

public class Water : DangerousElementalObject
{
    [SerializeField] private float slideForce = 25f;

    public override void GetInsideElement(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через лед.");
            PlayWalkingSound();
            if (playerAbility is IceAbility) return;
            SlipOnIce(playerAbility.gameObject);
        }
        else if (playerAbility is WaterAbility || playerAbility is IceAbility)
        {
            PlayWalkingSound();
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
        SetColliderTrigger(false);
        // GetComponent<Collider2D>().isTrigger = false;
    }

    private void DefrostWater()
    {
        if (currentState != ElementalObjectState.SecondState) return;

        Debug.Log("Вода разморожена");
        ChangeState(ElementalObjectState.FirstState);
        SetColliderTrigger(true);
        // GetComponent<Collider2D>().isTrigger = true;
    }

    private void DrownPlayer(GameObject player) => RespawnPlayer(player);

    private void SlipOnIce(GameObject player)
    {
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement == null) return;

        playerMovement.StartSliding(slideForce);
        Debug.Log("Игрок начал скользить по льду");
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != ElementalObjectState.SecondState) return;

        Movement playerMovement = collision.gameObject.GetComponent<Movement>();
        if (playerMovement == null) return;

        playerMovement.StopSliding();
        Debug.Log("Игрок перестал скользить по льду");
    }
}