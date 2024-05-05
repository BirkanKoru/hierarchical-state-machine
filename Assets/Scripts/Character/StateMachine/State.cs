using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateData stateData;

    public StateMachine stateMachine { get; protected set; }
    public StateMachineFactory stateMachineFactory { get; protected set; }
    public State superState { get; protected set; }
    public State subState { get; protected set; }

    public State(StateData stateData)
    {
        this.stateData = stateData;
        InitializeSubStates();
    }

    public abstract void InitializeSubStates();

    public abstract void Enter();

    public abstract void Exit();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void SwitchState(bool forceReset = false);

    public void LogicUpdateBranch()
    {
        LogicUpdate();
        subState?.LogicUpdateBranch();

        SwitchState();
    }

    public void PhysicsUpdateBranch()
    {
        PhysicsUpdate();
        subState?.PhysicsUpdateBranch();

        SwitchState();
    }

    public void ChangeState(string stateName, bool forceReset = false)
    {
        if(subState == null)
        {
            subState = stateMachineFactory.SetCurrState(stateName);
            subState.superState = this;

            stateMachine.Initialize(subState);

        } else {

            if(!stateMachineFactory.currStateName.Equals(stateName) || forceReset)
            {
                subState = stateMachineFactory.SetCurrState(stateName);
                subState.superState = this;

                stateMachine.ChangeState(subState, forceReset);
            }
        }
    }
}
