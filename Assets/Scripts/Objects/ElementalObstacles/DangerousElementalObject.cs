using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected PlayerSpawner playerSpawner;

    [SerializeField] protected AudioClip[] walkingSounds;
    private bool isWalkingSoundPlaying = false;

    protected override void Awake()
    {
        base.Awake();
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }

    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        Ability playerAbility = collision.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            StopWalkingSound();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        Ability playerAbility = collision.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            StopWalkingSound();
    }

    protected void PlayWalkingSound()
    {
        if (walkingSounds[(int)currentState] == null || isWalkingSoundPlaying) return;

        isWalkingSoundPlaying = true;
        audioSource.loop = false;
        audioSource.PlayOneShot(walkingSounds[(int)currentState]);

        //isWalkingSoundPlaying = false; //
    }

    protected void StopWalkingSound()
    {
        if (!isWalkingSoundPlaying) return;

        audioSource.Stop();
        isWalkingSoundPlaying = false;
    }

    protected void RespawnPlayer(GameObject player)
    {
        PlayWalkingSound();
        playerSpawner.Respawn(player);
    }
}