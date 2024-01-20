using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        stateMachine.Stamina.DrainStamina(stateMachine.StaminaCost);
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.onDashSound_UnityEvent.Invoke();
        stateMachine.MomentumVFX.Play();
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
            var keyboard = InputSystem.GetDevice<Keyboard>();
            if (keyboard != null && keyboard.leftShiftKey.isPressed)
            { stateMachine.SwitchState(new PlayerSprintingState(stateMachine));
                return;
            }

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
        stateMachine.MomentumVFX.Stop();
       
    }
    

   
}
