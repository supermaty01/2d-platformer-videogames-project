using UnityEngine;

[CreateAssetMenu(fileName = "InRangeAttack Check", menuName = "FSM/Decisions/InRangeAttack Check")]
public class InAttackRangeCheck : StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        float distance = (fms.Target.position - fms.transform.position).magnitude;
        float xDiff = fms.Target.position.x - fms.transform.position.x;

        return distance <= fms.Config.attackRange && Mathf.Sign(xDiff) == Mathf.Sign(fms.transform.localScale.x) && fms.Target.position.y >= fms.transform.position.y-1;
    }
}
