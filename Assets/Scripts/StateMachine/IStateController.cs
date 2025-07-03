using System.Collections.Generic;
using UnityEngine;

public class IStateController : MonoBehaviour
{
    protected IState _currentState = null;

    protected Dictionary<System.Type, IState> _stateTable;


    // Update is called once per frame
    void Update()
    {
        _currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        _currentState.PhysicsUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        _currentState = newState;
        _currentState.EnterState();
    }

    public void SetState(IState newState)
    {
        if(_currentState != null)
        {
            _currentState.ExitState();
        }

        SwitchOn(newState);
    }    

    public void SetState(System.Type newStateType)
    {
        SetState(_stateTable[newStateType]);
    }
}
