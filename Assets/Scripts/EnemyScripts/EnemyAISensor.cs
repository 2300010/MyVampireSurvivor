using System;
using UnityEngine;

public class EnemyAISensor : MonoBehaviour
{
    [SerializeField] float rangeRequiredToAttack;

    public float RangeToAttack { get => rangeRequiredToAttack; set => rangeRequiredToAttack = value; }

    public Action InRangeToAttackAction;
    public Action OutOfRangeToAttackAction;

    private void Update()
    {
        IsInRange();
    }

    private void IsInRange()
    {
        Vector2 playerPosition = new(PlayerManager.Instance().transform.position.x,
            PlayerManager.Instance().transform.position.y);
        Vector2 thisPosition = new(transform.position.x, transform.position.y);

        if (Vector2.Distance(playerPosition, thisPosition) <= rangeRequiredToAttack)
        {
            InRangeToAttackAction?.Invoke();
        }
        else
        {
            OutOfRangeToAttackAction?.Invoke();
        }
    }
}
