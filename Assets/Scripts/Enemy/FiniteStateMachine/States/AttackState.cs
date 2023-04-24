using UnityEngine;

public class AttackState : State
{
    public override StateType Type { get; }
    
    public AttackState() : base("Attack")
    {
    }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Attack");
        SetStateDuration(fms.Config.attackDuration);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}