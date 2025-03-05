using UnityEngine;

public abstract class DangerousElementalObject : ElementalObject
{
    protected RespawnManager respawnManager;
    [SerializeField] protected AudioClip[] walkingSounds;
    private bool isWalkingSoundPlaying = false;

    protected override void Awake()
    {
        base.Awake();
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Ability playerAbility = collision.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            GetInsideElement(playerAbility);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Ability playerAbility = other.gameObject.GetComponent<Ability>();
        if (playerAbility != null)
            StopWalkingSound();
    }

    private void OnTriggerExit2D(Collider2D collision)
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
    }

    protected void StopWalkingSound()
    {
        if (!isWalkingSoundPlaying) return;

        audioSource.Stop();
        isWalkingSoundPlaying = false;
    }
}