using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    PlayerControlsManager playerControlsManager;

    float walkSpeed = 2f;
    float runMultiplier = 3f;
    float rotationFactorPerFrame = 15f;

    //CharacterMovementState currMovementState;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector3 currMovement;
    float currSpeed = 2f;

    private void Start() 
    {
        playerControlsManager = PlayerControlsManager.Instance;

        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update() 
    {
        HandleAnimation();

        if(playerControlsManager.IsMovementPressed)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        if(!playerControlsManager.IsRunPressed)
            currSpeed = walkSpeed;
        else
            currSpeed = walkSpeed * runMultiplier;


        currMovement = playerControlsManager.CurrentMovement;
        currMovement.x *= currSpeed;
        currMovement.z *= currSpeed;
        HandleGravity();

        characterController.Move(currMovement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt = Vector3.zero;

        positionToLookAt.x = playerControlsManager.CurrentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = playerControlsManager.CurrentMovement.z;

        Quaternion currentRotation = this.transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        this.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if(playerControlsManager.IsMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);

        } else if(!playerControlsManager.IsMovementPressed && isWalking) {

            animator.SetBool(isWalkingHash, false);
        }

        if((playerControlsManager.IsMovementPressed && playerControlsManager.IsRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);

        } else if((!playerControlsManager.IsMovementPressed || !playerControlsManager.IsRunPressed) && isRunning) {

            animator.SetBool(isRunningHash, false);
        }
    }

    private void HandleGravity()
    {
        if(characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            currMovement.y = groundedGravity;

        } else {
            currMovement.y = Physics.gravity.y;
        }
    }
}
