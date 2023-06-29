using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMidAirJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("SecondJump");

    private const float CrossFadeDuration = 0.1f;


    public PlayerMidAirJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }



    public override void Enter()
    {
        IsMidAirJumped = true;

        stateMachine.ForceReceiver.VerticalVelocity = 0f;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * 10f);
       
        stateMachine.ForceReceiver.MidAirJump(stateMachine.MidAirJumpForce);
        

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
      


    }

    public override void Tick(float deltaTime)
    {
       
       
        Vector3 inAirMovement = CalculateMovementInAir();

        FaceMovementDirectionInAir(inAirMovement, deltaTime);

        Move(inAirMovement * stateMachine.AirMovementSpeed, deltaTime);

        stateMachine.transform.Translate(inAirMovement * deltaTime);






        if (stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine, IsMidAirJumped));
            return;
        }



    }

    public override void Exit()
    {
        IsMidAirJumped = true;
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
    
}
