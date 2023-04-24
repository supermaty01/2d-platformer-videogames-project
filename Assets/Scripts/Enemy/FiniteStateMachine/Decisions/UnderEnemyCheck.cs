using UnityEngine;

[CreateAssetMenu(fileName = "UnderEnemy Check", menuName = "FSM/Decisions/UnderEnemy Check")]
public class UnderEnemyCheck : StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        float distance = Mathf.Abs(fms.Target.position.x - fms.transform.position.x);
        return distance <= fms.Config.attackRange;
    }
}