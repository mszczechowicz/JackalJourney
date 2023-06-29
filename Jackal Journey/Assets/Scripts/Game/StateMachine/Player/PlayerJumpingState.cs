using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{

    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float CrossFadeDuration = 0.1f;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    

    public override void Enter()
    {
        IsMidAirJumped = false;
       
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);

        stateMachine.InputHandler.JumpEvent += OnJump;
        


    }

    public override void Tick(float deltaTime)
    {
        

        Vector3 inAirMovement = CalculateMovementInAir();

        FaceMovementDirectionInAir(inAirMovement, deltaTime);

        Move(inAirMovement * stateMachine.AirMovementSpeed, deltaTime);

        stateMachine.transform.Translate(inAirMovement * deltaTime);

        




        if (stateMachine.CharacterController.velocity.y <= 5)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine,IsMidAirJumped));
            return;
        }
        


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
        stateMachine.SwitchState(new PlayerMidAirJumpingState(stateMachine));
    }
}
