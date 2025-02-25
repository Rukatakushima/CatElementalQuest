using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AbilityCollider : MonoBehaviour
{
    public Ability abilityType;

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        ElementalObject elementalObject = other.GetComponent<Water>();
        if (elementalObject == null) elementalObject = other.GetComponent<Lava>();
        Debug.Log("Ability triggered. elementalObject: " + elementalObject);

        if (elementalObject != null && abilityType.isAbilityActive)
            elementalObject.InteractWithElement(abilityType);
    }
}