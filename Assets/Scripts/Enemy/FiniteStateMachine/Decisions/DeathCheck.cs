using UnityEngine;

[CreateAssetMenu(fileName = "Death Check", menuName = "FSM/Decisions/Death Check")]
public class DeathCheck : StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        return fms.Config.health <= 0;
    }
}