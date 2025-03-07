using System.Collections;
using UnityEngine;

public class Stone : ElementalObject
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private bool isOnCooldown = false;

    public override void InteractWithElement(Ability playerAbility)
    {
        if (isOnCooldown)
        {
            Debug.Log("Камень на кулдауне, изменение состояния невозможно.");
            return;
        }

        if (playerAbility is EarthAbility)
            HandleDamagedState();

        else if (playerAbility is IceAbility)
        {
            switch (currentState)
            {
                case ElementalObjectState.FirstState:
                    HandleNormalState();
                    StartCooldown();
                    break;
                case ElementalObjectState.SecondState:
                    HandleDamagedState();
                    break;
                default:
                    break;
            }
        }
    }

    private void HandleNormalState()
    {
        ChangeState(ElementalObjectState.SecondState);
        UpdateElementalObjectSprite();
    }

    private void HandleDamagedState()
    {
        OnStateChanged?.Invoke(ElementalObjectState.ThirdState);
        gameObject.SetActive(false);
    }

    private void StartCooldown()
    {
        if (isOnCooldown) return;

        isOnCooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
        Debug.Log("Кулдаун закончился, можно изменять состояние.");
    }
}