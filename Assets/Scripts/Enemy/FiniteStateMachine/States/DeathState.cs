using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State
{
    public override StateType Type { get; }
    
    public DeathState() : base("Death") { }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("Death");
        if (fms.Config.deathSoundName != null)
        {
            AudioManager.Instance.PlaySound2D(fms.Config.deathSoundName);
        }
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}