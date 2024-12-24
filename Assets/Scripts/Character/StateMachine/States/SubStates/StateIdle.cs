using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    public StateIdle(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
        
    }

    public override void Enter()
    {
        stateData.character.anim.SetBool("isWalking", false);
        stateData.character.anim.SetBool("isRunning", false);
    }

    public override void Exit()
    {
        
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void SwitchState(bool forceReset)
    {
        
    }
}
