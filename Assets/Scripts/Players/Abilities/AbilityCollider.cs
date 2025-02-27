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
                PhotonView elementalPhotonView = elementalObject.GetComponent<PhotonView>();
                if (elementalPhotonView != null)
                {
                    Debug.Log($"Передаём ViewID: {elementalPhotonView.ViewID}");
                    photonView.RPC("RPC_InteractWithElement", RpcTarget.All, elementalPhotonView.ViewID);
                }
                // photonView.RPC("RPC_InteractWithElement", RpcTarget.All, elementalObject.photonView.ViewID);
                
                // elementalObject.InteractWithElement(abilityType);

                gameObject.SetActive(false);
                Debug.Log("Обрабатываем взаимодействие с ElementalObject " + other);
            }
        }
    }

    [PunRPC]
    public void RPC_InteractWithElement(int elementalObjectViewID)
    {
        Debug.Log("elementalObjectViewID = " + elementalObjectViewID);
        PhotonView elementalPhotonView = PhotonView.Find(elementalObjectViewID);
        Debug.Log("elementalPhotonView = " + elementalPhotonView);
        if (elementalPhotonView != null)
        {
            ElementalObject elementalObject = elementalPhotonView.GetComponent<ElementalObject>();
            if (elementalObject != null)
            {
                elementalObject.InteractWithElement(abilityType);
            }
        }
    }
}