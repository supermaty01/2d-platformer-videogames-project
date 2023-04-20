public class RunState : State
{
    public override StateType Type => StateType.Run;

    public RunState() : base("Run") { }
    
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
