public class DeathState : State
{
    public DeathState() : base("Death")
    {
    }

    public override StateType Type { get; }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Death");
        if (fms.Config.deathSoundName != null) AudioManager.Instance.PlaySound2D(fms.Config.deathSoundName);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}