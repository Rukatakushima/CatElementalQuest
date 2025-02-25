using UnityEngine;

public class WindAbilityCollider : AbilityCollider
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (abilityType is WindAbility windAbility && other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                windAbility.ApplyWindForce(playerRigidbody);
            }
        }
    }
}