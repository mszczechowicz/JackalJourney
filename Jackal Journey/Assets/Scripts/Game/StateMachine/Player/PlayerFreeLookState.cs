using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

 
public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int VelocityHash = Animator.StringToHash("Velocity");
    

    private const float AnimatorDampTime = 0.1f;


    private readonly int FreeLookHash = Animator.StringToHash("PlayerFreeLookState");

    

    private const float CrossFadeDuration = 0.1f;

    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;

    public Transform InteractableObject;
    public float InteractionRange = 10f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookHash, CrossFadeDuration);

        stateMachine.InputHandler.JumpEvent += OnJump;
        stateMachine.InputHandler.DashEvent += OnDodge;
        stateMachine.InputHandler.TargetEvent += OnTarget;
    }


    #region Tick
    
    public override void Tick(float deltaTime)
    {
#region Interactions

        if (stateMachine.InputHandler.IsInteracting && stateMachine.InteractionHandler.GetInteractableObject() != null)
        {                 
          
           stateMachine.SwitchState(new PlayerInteractingState(stateMachine));
           return;
        }

#endregion

        if (stateMachine.InputHandler.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (!stateMachine.ForceReceiver.IsGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine, IsMidAirJumped));
            return;
        }


        Vector3 movement = CalculateMovement(deltaTime);




        Move(CalculateSlope(movement) * stateMachine.FreeLookMovementSpeed, deltaTime);


        if (stateMachine.InputHandler.MovementValue == Vector2.zero)
        {

            stateMachine.Animator.SetFloat(VelocityHash, 0, AnimatorDampTime, deltaTime);
            stateMachine.IK.enabled = true;
            return;
        }
        stateMachine.Animator.SetFloat(VelocityHash, 1, AnimatorDampTime, deltaTime);
        stateMachine.IK.enabled = false;


        FaceMovementDirection(movement, deltaTime);


    }
    #endregion
    public override void Exit()
    {

        stateMachine.InputHandler.JumpEvent -= OnJump;
        stateMachine.InputHandler.DashEvent -= OnDodge;
        stateMachine.InputHandler.TargetEvent -= OnTarget;

    }
    #region Movement_Data
    private Vector3 CalculateMovement(float deltaTime)
    {

        Vector3 movement = new Vector3();

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movement = forward * stateMachine.InputHandler.MovementValue.y + right * stateMachine.InputHandler.MovementValue.x;

        return movement;
    }
    private void FaceMovementDirection(Vector3 movement, float deltatime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltatime * stateMachine.RotationDamping);
    }

    private Vector3 CalculateSlope(Vector3 movement)
    {
        return Vector3.ProjectOnPlane(movement, stateMachine.ForceReceiver.HitInfo.normal).normalized;
    }
    #endregion
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    #region Dash_Data


    private void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.InputHandler.MovementValue.normalized));      
    }

    #endregion

    private void OnTarget()
    {

        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    
    }
    
}
