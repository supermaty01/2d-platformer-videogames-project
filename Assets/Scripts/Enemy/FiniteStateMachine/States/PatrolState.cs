using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolState : State
{
    private bool movingToFinal = true;
    public override StateType Type { get; }
    
    public PatrolState() : base("Patrol") { }

    protected override void OnEnterState(FiniteStateMachine fms)
    {
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        if (fms.transform.position.x >= fms.Config.finalPos.x)
        {
            movingToFinal = false;
            Flip(fms);
        }
        else if (fms.transform.position.x <= fms.Config.initialPos.x)
        {
            movingToFinal = true;
            Flip(fms);
        }

        Vector3 targetPos = movingToFinal ? fms.Config.finalPos : fms.Config.initialPos;
        fms.transform.position = Vector3.MoveTowards(fms.transform.position, targetPos, fms.Config.speed * deltaTime);
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }

	public void Flip(FiniteStateMachine fms)
    {
        Vector3 localScale = fms.transform.localScale;
		localScale.x *= -1;
		fms.transform.localScale = localScale;
    }
}
