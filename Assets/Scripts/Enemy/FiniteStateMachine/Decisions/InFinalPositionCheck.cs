using UnityEngine;

[CreateAssetMenu(fileName = "InFinalPosition Check", menuName = "FSM/Decisions/InFinalPosition Check")]
public class InFinalPositionCheck : StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        if (fms.Config.speed < 0) return fms.transform.position.y <= fms.Config.finalPos.y;
        return fms.transform.position.y >= fms.Config.initialPos.y;
    }
}