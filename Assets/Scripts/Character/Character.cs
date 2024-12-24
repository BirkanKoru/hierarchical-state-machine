using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float crouchedWalkSpeed = 2f;

    public Animator anim { get; protected set; }
    public CharacterController characterController { get; protected set; }

    public float WalkSpeed { get { return walkSpeed; }}
    public float RunSpeed { get { return runSpeed; }}
    public float CrouchedWalkSpeed { get { return crouchedWalkSpeed; }}

    //Movement Variables
    public Vector3 currMovement { get; set; }
    public Vector3 appliedMovement { get; set; }
    public float currSpeed { get; set; }
    public float rotationFactorPerFrame { get; set; }

    //Gravity Variables
    public float gravity { get; set; } = -9.8f;
    public float groundedGravity { get; set; } = -0.05f;

    //Jump Variables
    private float initialJumpVelocity = 0f;
    private float maxJumpHeight = 3.0f;
    private float maxJumpTime = 0.75f;

    public bool IsGrounded { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsFalling { get; private set; }

    public bool IsJumped { get; private set; }
    public bool IsCrouched { get; private set; }

    private void Start() 
    {
        
    }

    private void Update() 
    {
        
    }

    public void HandleBooleans(bool IsMoving = false, bool IsRunning = false, bool IsJumping = false, bool IsCrouched = false)
    {
        this.IsMoving = IsMoving;
        this.IsRunning = IsRunning;
        this.IsJumping = IsJumping;
        this.IsCrouched = IsCrouched;
    }

    public void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    public void HandleJump()
    {
        if(!IsJumped && characterController.isGrounded && IsJumping)
        {
            IsJumped = true;
            currMovement = new Vector3(currMovement.x, initialJumpVelocity * 0.5f, currMovement.z);

        } else if(IsJumped && characterController.isGrounded) {

            IsJumped = false;
        }
    }

    public void HandleRotation()
    {
        Vector3 positionToLookAt = Vector3.zero;

        positionToLookAt.x = currMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currMovement.z;

        if(positionToLookAt != Vector3.zero)
        {
            Quaternion currentRotation = this.transform.rotation;

            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            this.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    public void HandleMovement()
    {
        Vector3 dir = currMovement;
        dir.x *= currSpeed;
        dir.z *= currSpeed;
        dir.y = currMovement.y;
        
        appliedMovement = dir;

        characterController.Move(appliedMovement * Time.deltaTime);
    }

    public void HandleGravity()
    {
        IsFalling = currMovement.y <= 0.0f || !IsJumping;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            currMovement = new Vector3(currMovement.x, groundedGravity, currMovement.z);

        } else if(IsFalling) {

            float previousYVelocity = currMovement.y;
            float newYVelocity = currMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20.0f);

            currMovement = new Vector3(currMovement.x, nextYVelocity, currMovement.z);

        } else {

            float previousYVelocity = currMovement.y;
            float newYVelocity = currMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;

            currMovement = new Vector3(currMovement.x, nextYVelocity, currMovement.z);
        }
    }
}