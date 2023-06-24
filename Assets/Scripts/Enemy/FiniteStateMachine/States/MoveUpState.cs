using UnityEngine;

public class MoveUpState : State
{
    public MoveUpState() : base("MoveUp")
    {
    }

    public override StateType Type { get; }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
        fms.Config.speed = -fms.Config.speed;
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        fms.transform.position += Vector3.up * (fms.Config.speed * 0.2f * deltaTime);
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}