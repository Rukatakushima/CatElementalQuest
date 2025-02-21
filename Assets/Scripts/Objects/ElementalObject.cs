using UnityEngine;
using UnityEngine.Events;

public abstract class ElementalObject : MonoBehaviour
{
    protected enum ElementalObjectState { FirstState, SecondState, ThirdState }
    protected ElementalObjectState currentState = ElementalObjectState.FirstState;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] stateSprites;
    protected UnityEvent<ElementalObjectState> OnStateChanged; // События, что будут происходить при смене состояния в зависимости от этого состояния

    protected virtual void Awake()
    {
        OnStateChanged = new UnityEvent<ElementalObjectState>(); // Инициализация события
    }
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateElementalObjectSprite();
        OnStateChanged.AddListener(HandleStateChangeEvent);
    }

    public abstract void HandleInstantInteraction(Ability playerAbility);

    public abstract void HandleContinuousInteraction(Ability playerAbility);

    // protected virtual void HandleTriggerInteraction(Collider2D other) { }

    protected virtual void ChangeState(ElementalObjectState newState)
    {
        currentState = newState;
        UpdateElementalObjectSprite();

        OnStateChanged?.Invoke(newState);
    }

    protected void UpdateElementalObjectSprite()
    {
        spriteRenderer.sprite = stateSprites[(int)currentState];
    }

    protected virtual void HandleStateChangeEvent(ElementalObjectState newState) => Debug.Log($"Состояние изменено на: {newState}");
}