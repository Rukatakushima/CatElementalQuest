using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AbilityCollider : MonoBehaviour
{
    public Ability abilityType;
    private Collider2D abilityCollider;

    private void Awake() => abilityCollider = GetComponent<Collider2D>();

    // private void Update()
    // {
    //     if (abilityType.isAbilityActive)
    //         CheckForElementalObjects();
    // }

    public void CheckForElementalObjects()
    {
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D().NoFilter(); // Фильтр для всех коллайдеров

        int collisionCunt = abilityCollider.OverlapCollider(filter, results);

        for (int i = 0; i < collisionCunt; i++)
        {
            Collider2D other = results[i];
            Debug.Log($"Обнаружено пересечение с объектом: {other.name}");

            ElementalObject elementalObject = other.GetComponent<ElementalObject>();

            if (elementalObject != null)
            {
                Debug.Log("Обрабатываем взаимодействие с ElementalObject " + other);
                elementalObject.InteractWithElement(abilityType);

                gameObject.SetActive(false);
            }
        }
    }

    /*
    protected void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"OnTriggerStay2D вызван с объектом: {other.name}");

        ElementalObject elementalObject = other.GetComponent<ElementalObject>();

        if (elementalObject != null && abilityType.isAbilityActive)
        {
            Debug.Log("Обрабатываем взаимодействие с ElementalObject " + other);
            elementalObject.InteractWithElement(abilityType);
        }
    }
    */
}