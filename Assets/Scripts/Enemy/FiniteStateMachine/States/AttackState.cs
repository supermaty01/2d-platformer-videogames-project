using UnityEngine;

public class AttackState : State
{
    public override StateType Type { get; }

    private float _attackDelay;

    public AttackState() : base("Attack")
    {
    }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Attack");
        _attackDelay = fms.Config.attackDelay;
        SetStateDuration(fms.Config.attackDuration);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}