using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected RespawnManager respawnManager;

    protected override void Start()
    {
        base.Start();
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Ability playerAbility = collision.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }
}