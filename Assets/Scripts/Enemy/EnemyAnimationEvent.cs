using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Action OnAttackAction;
    public Action OnDestroyAction;

    public void AttackEvent()
    {
        OnAttackAction?.Invoke();
    }

    public void DestroyEvent()
    {
        OnDestroyAction?.Invoke();
    }
}