public class DeathState : State
{
    public override StateType Type { get; }
    
    public DeathState() : base("Death") { }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Death");
        SetStateDuration(0.833f);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
        fms.gameObject.SetActive(false);
    }
}