using UnityEngine;

public class Water : DangerousElementalObject
{
    [SerializeField] private float slideForce = 25f;

    public override void GetInsideElement(Ability playerAbility)
    {
        if (currentState == ElementalObjectState.SecondState)
        {
            Debug.Log("Любой игрок безопасно проходит через лед.");
            if (playerAbility is IceAbility) return;
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
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement == null) return;

        // Vector2 slideDirection = playerMovement.GetComponent<Rigidbody2D>().velocity.normalized;
        playerMovement.StartSliding(slideForce);
        Debug.Log("Игрок начал скользить по льду");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != ElementalObjectState.SecondState) return;

        Movement playerMovement = collision.gameObject.GetComponent<Movement>();
        if (playerMovement == null) return;

        playerMovement.StopSliding();
        Debug.Log("Игрок перестал скользить по льду");
    }

    protected override void StateChangeEvent(ElementalObjectState newState) => Debug.Log($"Состояние воды изменено на: {newState}");
}