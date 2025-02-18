using UnityEngine;

public abstract class ElementalObject : MonoBehaviour
{
    protected enum ElementalObjectState { FirstState, SecondState, ThirdState }
    protected ElementalObjectState currentState = ElementalObjectState.FirstState;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] stateSprites;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void HandleInstantInteraction(Ability playerAbility);
    public abstract void HandleContinuousInteraction(Ability playerAbility);
    protected void UpdateElementalObjectSprite()
    {
        spriteRenderer.sprite = stateSprites[(int)currentState];
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleInstantInteraction(collisionAbility);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleContinuousInteraction(collisionAbility);
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleInstantInteraction(collisionAbility);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        Ability collisionAbility = collision.gameObject.GetComponent<Ability>();

        if (collisionAbility)
            HandleContinuousInteraction(collisionAbility);
    }

}