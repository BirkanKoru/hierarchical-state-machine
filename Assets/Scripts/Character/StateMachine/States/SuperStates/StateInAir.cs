using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInAir : State
{
    public StateInAir(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
        
    }

    public override void Enter()
    {
        stateData.character.anim.SetBool("isJumping", true);
    }

    public override void Exit()
    {
        stateData.character.anim.SetBool("isJumping", false);
    }

    public override void LogicUpdate()
    {
        stateData.character.HandleRotation();
        stateData.character.HandleMovement();
        stateData.character.HandleGravity();
        stateData.character.HandleJump();
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void SwitchState(bool forceReset)
    {
        
    }
}
