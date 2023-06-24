public class StateTransition
{
    public StateDecision Decision;
    public StateType TargetState;

    public bool Check(FiniteStateMachine fms)
    {
        if (Decision != null)
            return Decision.Check(fms);

        return true;
    }
}