using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected RespawnManager respawnManager;

    protected override void Start()
    {
        base.Start();
        respawnManager = FindObjectOfType<RespawnManager>();
    }
}