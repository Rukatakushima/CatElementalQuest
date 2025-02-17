using UnityEngine;

public abstract class ElementalObject : MonoBehaviour
{
    protected enum ElementalObjectState { FirstState, SecondState, ThirdState }
    protected ElementalObjectState currentState = ElementalObjectState.FirstState;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] stateSprites;

    public abstract void HandleInstantInteraction(Ability playerAbility);
    public abstract void HandleContinuousInteraction(Ability playerAbility);
    protected void UpdateElementalObjectSprite()
    {
        spriteRenderer.sprite = stateSprites[(int)currentState];
    }
}