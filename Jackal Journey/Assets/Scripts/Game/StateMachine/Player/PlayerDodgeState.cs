using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{

    private readonly int DodgeHash = Animator.StringToHash("Dodge");

    private const float CrossFadeDuration = 0.1f;

    private float remainingDodgeTime;

    private Vector3 dodgingDirectionInput;
    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine) 
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Health.SetInvulnerable(true);

        stateMachine.Animator.CrossFadeInFixedTime(DodgeHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        if (dodgingDirectionInput == Vector3.zero)
        {
            movement += stateMachine.MainCameraTransform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        }
        else
        {
            movement += stateMachine.MainCameraTransform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.MainCameraTransform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        }

        Move(movement,deltaTime);




        remainingDodgeTime -= deltaTime;
        if (remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
    

   
}
