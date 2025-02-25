using UnityEngine;
using System.Collections.Generic;

public class AbilityColliderPool : MonoBehaviour
{
    [SerializeField] private GameObject abilityColliderPrefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> abilityColliders;

    private void Awake()
    {
        abilityColliders = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject collider = CreateNewAbilityColliderPool();
            collider.SetActive(false);
        }
    }

    public GameObject GetAbilityCollider()
    {
        foreach (GameObject collider in abilityColliders)
        {
            if (!collider.activeInHierarchy)
            {
                collider.SetActive(true);
                return collider;
            }
        }

        return CreateNewAbilityColliderPool(); ;
    }

    public void ReturnAbilityCollider(GameObject collider)
    {
        collider.SetActive(false);
    }

    public GameObject CreateNewAbilityColliderPool()
    {
        GameObject collider = Instantiate(abilityColliderPrefab, transform);
        abilityColliders.Add(collider);
        return collider;
    }
}