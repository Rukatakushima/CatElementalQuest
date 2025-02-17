using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    // [SerializeField] protected RespawnManager respawnManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseAbility();
        }
    }

    public abstract void UseAbility();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ElementalObject elementalObject = collision.gameObject.GetComponent<ElementalObject>();
        if (elementalObject != null)
        {
            elementalObject.HandlePlayerCollision(this);
        }
    }
}
