using System;
using UnityEngine;
using UnityEngine.Events;

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