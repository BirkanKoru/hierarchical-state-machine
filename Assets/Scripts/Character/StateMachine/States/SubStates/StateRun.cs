using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : State
{
    public StateRun(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
       
    }

    public override void Enter()
    {
        stateData.character.anim.SetBool("isWalking", true);
        stateData.character.anim.SetBool("isRunning", true);
        stateData.character.currSpeed = 6f;
    }

    public override void Exit()
    {
        stateData.character.anim.SetBool("isRunning", false);
    }

    public override void LogicUpdate()
    {
        stateData.character.HandleRotation();
    }

    public override void PhysicsUpdate()
    {

    }

    public override void SwitchState(bool forceReset)
    {
        
    }
}
