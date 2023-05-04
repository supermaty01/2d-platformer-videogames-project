using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Animator anim;
    
    [Space(10)]
    [SerializeField] 
    private Transform target;
    private EnemyConfig _config;

    public Transform Target => target;
    public EnemyConfig Config => _config;

    private readonly Dictionary<StateType, State> _statesDic = new();
    private StateType _currentState;
    
    private void Start()
    {
        _config = GetComponent<EnemyConfig>();

        Bind(_config.fsmData);
        ToState(_config.initialState);
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
        if(newState == _currentState)
            return;
        
        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnExit(this);
        }
        
        _currentState = newState;

        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnEnter(this);
        }
    }

    private void Bind(FSMData fsmData)
    {
        foreach (var stateData in fsmData.States)
        {
            var state = State.CreateState(stateData.StateType);
            if(state == null)
                continue;

            foreach (var transitionData in stateData.Transition)
            {
                state.AddTransition(transitionData.TargetState, transitionData.Decision);
            }
            
            _statesDic.Add(stateData.StateType, state);
        }
    }
}
