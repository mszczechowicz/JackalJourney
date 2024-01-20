using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintingState : PlayerBaseState
{
    public PlayerSprintingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int SprintingHash = Animator.StringToHash("Sprint");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        stateMachine.MomentumVFX.Play();
        stateMachine.Animator.CrossFadeInFixedTime(SprintingHash, CrossFadeDuration);
        stateMachine.InputHandler.JumpEvent += OnJump;
        stateMachine.InputHandler.DashEvent += OnDodge;
        stateMachine.Stamina.IsSprinting = true;
    }
    public override void Tick(float deltaTime)
    {

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
        var keyboard = InputSystem.GetDevice<Keyboard>();
        if (keyboard != null && keyboard.leftShiftKey.wasReleasedThisFrame)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        if (stateMachine.Stamina.GetStamina() <= 5)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        stateMachine.Stamina.DrainStaminaPerSecond(stateMachine.SprintStaminaCost);


        Vector3 movement = CalculateMovement(deltaTime);


        
        





        Move(CalculateSlope(movement) * stateMachine.SprintingMovementSpeed, deltaTime);


        if (stateMachine.InputHandler.MovementValue == Vector2.zero)
        {

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        stateMachine.onMoveSound_UnityEvent.Invoke();//do sprawdzenia
       
        stateMachine.IK.enabled = false;


        FaceMovementDirection(movement, deltaTime);


    }
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
    public override void Exit()
    {
        stateMachine.InputHandler.JumpEvent -= OnJump;
        stateMachine.InputHandler.DashEvent -= OnDodge;
        stateMachine.Stamina.IsSprinting = false;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

  


    private void OnDodge()
    {
        if (stateMachine.Stamina.GetStamina() < stateMachine.StaminaCost) { return; }
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.InputHandler.MovementValue.normalized));
    }
   
  
}
