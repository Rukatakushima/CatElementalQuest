using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PhotonView))]
public class AbilityCollider : MonoBehaviourPunCallbacks
{
    public Ability abilityType;
    private Collider2D abilityCollider;

    private void Awake() => abilityCollider = GetComponent<Collider2D>();

    // private void Update() => CheckForElementalObjects();

    public void CheckForElementalObjects()
    {
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D().NoFilter(); // Фильтр для всех коллайдеров

        int collisionCunt = abilityCollider.OverlapCollider(filter, results);

        for (int i = 0; i < collisionCunt; i++)
        {
            Collider2D other = results[i];

            ElementalObject elementalObject = other.GetComponent<ElementalObject>();

            if (elementalObject != null)
                elementalObject.InteractWithElement(abilityType);
        }
    }
}