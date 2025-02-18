using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    [SerializeField] protected RespawnManager respawnManager;

    protected override void Start()
    {
        base.Start();
        respawnManager = FindAnyObjectByType<RespawnManager>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleInstantInteraction(collisionAbility);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleContinuousInteraction(collisionAbility);
    }
}