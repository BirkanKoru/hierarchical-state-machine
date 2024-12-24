using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : State
{
    public StateWalk(StateData stateData) 
    : base(stateData) { }

    public override void InitializeSubStates()
    {
        
    }

    public override void Enter()
    {
        stateData.character.anim.SetBool("isWalking", true);

        stateData.character.currSpeed = stateData.character.IsCrouched ? stateData.character.CrouchedWalkSpeed : stateData.character.WalkSpeed;
        stateData.character.rotationFactorPerFrame = 15f;
    }

    public override void Exit()
    {
        stateData.character.anim.SetBool("isWalking", false);
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
