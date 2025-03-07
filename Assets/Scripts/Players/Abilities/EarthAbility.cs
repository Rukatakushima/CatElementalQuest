using UnityEngine;

public class EarthAbility : Ability
{
    protected override void Awake()
    {
        base.Awake();
        shouldDeactiveCollider = false;
    }

    private void Start() => abilityObject.GetComponent<Collider2D>().isTrigger = false;

    public override void UseAbility() => Debug.Log("Земля: Разрушаю преграду!");
}