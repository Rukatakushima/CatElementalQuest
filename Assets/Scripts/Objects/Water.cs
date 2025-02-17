using UnityEngine;

public class Water : ElementalObject
{
    [SerializeField] protected RespawnManager respawnManager;

    public override void HandlePlayerCollision(Ability playerAbility)
    {
        if (playerAbility is WaterAbility)
        {
            Debug.Log("Водный игрок безопасно проходит через воду.");
        }
        else
        {
            Debug.Log("Игрок утонул!");
            respawnManager.Respawn(playerAbility.gameObject);
        }
    }
}