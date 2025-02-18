using UnityEngine;

public class AbilityCollider : MonoBehaviour
{
    private Ability playerAbility;

    private void Start()
    {
        playerAbility = transform.parent.GetComponentInParent<Ability>();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     ElementalObject elementalObject = other.GetComponent<ElementalObject>();
    //     if (elementalObject != null && playerAbility.isAbilityActive)
    //     {
    //         elementalObject.HandleContinuousInteraction(playerAbility);
    //     }
    // }

    private void OnTriggerStay2D(Collider2D other)
    {
        ElementalObject elementalObject = other.GetComponent<ElementalObject>();
        if (elementalObject != null && playerAbility.isAbilityActive)
        {
            elementalObject.HandleContinuousInteraction(playerAbility);
        }
    }
}