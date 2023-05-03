using System;
using UnityEngine;

[Serializable]
public class PatrolState : State
{
    public PatrolState() : base("Patrol")
    {
    }

    public override StateType Type { get; }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        if (fms.transform.position.x >= fms.Config.finalPos.x ||
            fms.transform.position.x <= fms.Config.initialPos.x) Flip(fms);

        fms.transform.position += fms.transform.localScale.x * (fms.Config.speed * deltaTime) * Vector3.right;
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }

    public void Flip(FiniteStateMachine fms)
    {
        var localScale = fms.transform.localScale;
        localScale.x *= -1;
        fms.transform.localScale = localScale;
    }
}