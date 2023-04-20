public class FallState : State
{
    public override StateType Type => StateType.Fall;

    public FallState() : base("Fall") { }
    
    protected override void OnEnterState(FiniteStateMachine fms)
    {
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}
