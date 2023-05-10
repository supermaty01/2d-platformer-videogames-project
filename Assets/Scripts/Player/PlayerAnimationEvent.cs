using System;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
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
