using System;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public Action OnAttackAction;
    public Action OnDestroyAction;
    public Action OnFirstStepAction;
    public Action OnSecondStepAction;
    public void AttackEvent()
    {
        OnAttackAction?.Invoke();
    }
    
    public void DestroyEvent()
    {
        OnDestroyAction?.Invoke();
    }
    
    public void FirstStepEvent()
    {
        OnFirstStepAction?.Invoke();
    }
    
    public void SecondStepEvent()
    {
        OnSecondStepAction?.Invoke();
    }
}
