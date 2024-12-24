using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager Instance { get; private set; }

    private PlayerInput playerInput;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement = Vector3.zero;

    public Vector3 CurrentMovement { get { return currentMovement; } private set { CurrentMovement = value; } }

    public bool IsJumpPressed { get; private set; }
    public bool IsCrouchPressed { get; private set; }

    public bool IsMovementPressed { get; private set; }
    public bool IsRunPressed { get; private set; }
    

    private void Awake() 
    {
        if(Instance == null) Instance = this;

        playerInput = new PlayerInput();
        StartListening();
    }

    void StartListening()
    {
        playerInput.CharacterControls.Movement.started += OnMovementInput;
        playerInput.CharacterControls.Movement.canceled += OnMovementInput;
        playerInput.CharacterControls.Movement.performed += OnMovementInput;

        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        playerInput.CharacterControls.Crouch.started += OnCrouch;
        playerInput.CharacterControls.Crouch.canceled += OnCrouch;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();

        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

        IsMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        IsRunPressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }

    void OnCrouch(InputAction.CallbackContext context)
    {
        IsCrouchPressed = context.ReadValueAsButton();
    }

    private void OnEnable() 
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable() 
    {
        playerInput.CharacterControls.Disable();
    }
}
