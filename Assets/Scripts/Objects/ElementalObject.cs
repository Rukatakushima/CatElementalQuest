using UnityEngine;

public abstract class ElementalObject : MonoBehaviour
{
    public abstract void HandlePlayerCollision(Ability playerAbility);
}