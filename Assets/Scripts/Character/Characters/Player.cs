using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public StateMachineFactory stateMachineFactory { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public StateData stateData { get; private set; }

    void Awake() 
    {
        GetReqComponents();
        CreateStateMachine();

        State state = stateMachineFactory.SetCurrState(nameof(StateGrounded));
        stateMachine.Initialize(state);
    }

    private void Start() 
    {
        SetupJumpVariables();
    }

    void Update()
    {
        stateMachine.CurrState.LogicUpdateBranch();
        SwitchState();
    }

    void FixedUpdate() 
    {
        stateMachine.CurrState.PhysicsUpdateBranch();
    }

    private void GetReqComponents()
    {
        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void CreateStateMachine()
    {
        stateData = new StateData(character: this);

        stateMachineFactory = new StateMachineFactory();
        stateMachine = new StateMachine(stateMachineFactory);

        stateMachineFactory.AddStates(new Dictionary<string, State>() 
        {
            { nameof(StateGrounded), new StateGrounded(stateData) },
            { nameof(StateInAir), new StateInAir(stateData) }
            
        });
    }

    private void SwitchState(bool forceReset = false)
    {
        if(!IsJumped)
        {
            ChangeState(nameof(StateGrounded), forceReset);

        } else {

            ChangeState(nameof(StateInAir), forceReset);
        }
    }

    public void ChangeState(string stateName, bool forceReset = false)
    {
        if(!stateMachineFactory.currStateName.Equals(stateName))
        {
            State state = stateMachineFactory.SetCurrState(stateName);
            stateMachine.ChangeState(state, forceReset);
        }
    }
}
