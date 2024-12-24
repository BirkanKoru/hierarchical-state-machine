using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGrounded : State
{
    public StateGrounded(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
        stateMachineFactory = new StateMachineFactory();
        stateMachine = new StateMachine(stateMachineFactory);

        stateMachineFactory.AddStates(new Dictionary<string, State>() 
        {
                { nameof(StateStand), new StateStand(stateData) },
                { nameof(StateCrouch), new StateCrouch(stateData) },
        });
    }
    
    public override void Enter()
    {
        stateData.character.anim.SetBool("isGrounded", true);
    }

    public override void Exit()
    {
        stateData.character.anim.SetBool("isGrounded", false);
    }

    public override void LogicUpdate()
    {
        if(!stateData.character.IsCrouched)
        {
            ChangeState(nameof(StateStand));

        } else {

            ChangeState(nameof(StateCrouch));
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void SwitchState(bool forceReset)
    {
        
    }
}
