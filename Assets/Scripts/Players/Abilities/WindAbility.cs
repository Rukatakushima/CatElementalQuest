using UnityEngine;

public class WindAbility : Ability
{
    [SerializeField] private float windForce = 10f;
    [SerializeField] private Vector2 windDirection = Vector2.up;

    public override void UseAbility()
    {
        Debug.Log("Воздух: Создаю ветряной поток!");
    }

    public void ApplyWindForce(Rigidbody2D targetRigidbody)
    {
        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce(windDirection * windForce, ForceMode2D.Impulse);
            Debug.Log($"Применена сила ветра к {targetRigidbody.gameObject.name}");
        }
    }
}