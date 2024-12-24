using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStand : State
{
    public StateStand(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
        stateMachineFactory = new StateMachineFactory();
        stateMachine = new StateMachine(stateMachineFactory);

        stateMachineFactory.AddStates(new Dictionary<string, State>() 
        {
                { nameof(StateIdle), new StateIdle(stateData) },
                { nameof(StateWalk), new StateWalk(stateData) },
                { nameof(StateRun), new StateRun(stateData) }
        });
    }

    public override void Enter()
    {
        stateData.character.anim.SetBool("isCrouched", false);
        SwitchState(true);
    }

    public override void Exit()
    {
        
    }

    public override void LogicUpdate()
    {
        stateData.character.HandleMovement();
        stateData.character.HandleGravity();
        stateData.character.HandleJump();
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void SwitchState(bool forceReset)
    {
        if(stateData.character.IsMoving)
        {
            if(stateData.character.IsRunning)
            {
                ChangeState(nameof(StateRun), forceReset);

            } else {
                ChangeState(nameof(StateWalk), forceReset);
            }

        } else {
            ChangeState(nameof(StateIdle), forceReset);
        }
    }
}
