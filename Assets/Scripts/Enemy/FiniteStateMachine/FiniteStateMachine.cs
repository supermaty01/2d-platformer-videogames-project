using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [Space(10)] [SerializeField] private Animator anim;

    private readonly Dictionary<StateType, State> _statesDic = new();
    private StateType _currentState;

    public Transform Target { get; private set; }

    public EnemyConfig Config { get; private set; }

    private void Start()
    {
        Config = GetComponent<EnemyConfig>();
        Target = GameManager.Instance.target;

        Bind(Config.fsmData);
        ToState(Config.initialState);
    }

    private void Update()
    {
        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnUpdate(this, Time.deltaTime);
            _statesDic[_currentState].CheckTransition(this, Time.deltaTime);
        }
    }

    public void TriggerAnimation(string animation)
    {
        anim.SetTrigger(animation);
    }

    public void ToState(StateType newState)
    {
        if (newState == _currentState)
            return;

        if (_statesDic.ContainsKey(_currentState)) _statesDic[_currentState].OnExit(this);

        _currentState = newState;

        if (_statesDic.ContainsKey(_currentState)) _statesDic[_currentState].OnEnter(this);
    }

    private void Bind(FSMData fsmData)
    {
        foreach (var stateData in fsmData.States)
        {
            var state = State.CreateState(stateData.StateType);
            if (state == null)
                continue;

            foreach (var transitionData in stateData.Transition)
                state.AddTransition(transitionData.TargetState, transitionData.Decision);

            _statesDic.Add(stateData.StateType, state);
        }
    }
}