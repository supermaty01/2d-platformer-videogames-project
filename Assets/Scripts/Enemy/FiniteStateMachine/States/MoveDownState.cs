using UnityEngine;

public class MoveDownState : State
{
    public override StateType Type { get; }
    
    public MoveDownState() : base("MoveDown") { }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.TriggerAnimation("MoveDown");
        fms.Config.speed = - fms.Config.speed;
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        fms.transform.position += Vector3.up * (fms.Config.speed * deltaTime);
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}