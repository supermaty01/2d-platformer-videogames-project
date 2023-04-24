using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Action OnAttackAction;

    public void AttackEvent()
    {
        OnAttackAction?.Invoke();
    }
}