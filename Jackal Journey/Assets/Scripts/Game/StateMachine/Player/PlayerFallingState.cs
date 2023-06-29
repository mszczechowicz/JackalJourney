using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{

    private readonly int IdleFallHash = Animator.StringToHash("IdleFall");

    private const float CrossFadeDuration = 0.5f;


    public PlayerFallingState(PlayerStateMachine stateMachine,bool IsJumped) : base(stateMachine)
    { 
    IsMidAirJumped = IsJumped;
    }



    public override void Enter()
    {
        stateMachine.InputHandler.JumpEvent += OnJump;
        
        stateMachine.Animator.CrossFadeInFixedTime(IdleFallHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.ForceReceiver.IsGrounded)
        {
            ReturnToLocomotion();
        }


        Vector3 inAirMovement = CalculateMovementInAir();
        FaceMovementDirectionInAir(inAirMovement, deltaTime);

        Move(inAirMovement * stateMachine.AirMovementSpeed, deltaTime);
        stateMachine.transform.Translate(inAirMovement * deltaTime);
      


    }

    public override void Exit()
    {
        stateMachine.InputHandler.JumpEvent -= OnJump;
       
    }

    private Vector3 CalculateMovementInAir()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputHandler.MovementValue.y + right * stateMachine.InputHandler.MovementValue.x;


    }

    private void FaceMovementDirectionInAir(Vector3 movement, float deltatime)
    {

        if (movement != Vector3.zero)
        {

            stateMachine.transform.rotation = Quaternion.Lerp
                (stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltatime * stateMachine.RotationDamping);
        }
    }

    private void OnJump()
    {
        if (!IsMidAirJumped)
        {
            
            stateMachine.SwitchState(new PlayerMidAirJumpingState(stateMachine));
            return;
        }
        return;
    }
}
