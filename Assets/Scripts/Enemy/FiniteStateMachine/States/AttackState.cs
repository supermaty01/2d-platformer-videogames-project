public class AttackState : State
{
    public AttackState() : base("Attack")
    {
    }

    public override StateType Type { get; }

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