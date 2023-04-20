public class JumpState : State
{
    public override StateType Type => StateType.Jump;

    public JumpState() : base("Jump") { }
    
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
