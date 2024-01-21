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
        stateMachine.CharacterController.excludeLayers += LayerMask.GetMask("Spider");
        CalculateDodgeDirectionPlayer();

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
        stateMachine.CharacterController.excludeLayers -= LayerMask.GetMask("Spider");

    }

  
    private void CalculateDodgeDirectionPlayer()
    {
        // Pobierz oryginaln¹ rotacjê postaci
        Quaternion originalRotation = stateMachine.transform.rotation;

        // SprawdŸ, czy jest wprowadzenie z klawiatury
        if (!Mathf.Approximately(stateMachine.InputHandler.MovementValue.sqrMagnitude, 0f))
        {
            // Pobierz kierunek forward z kamery, zresetuj sk³adow¹ y, aby uzyskaæ tylko obroty w p³aszczyŸnie poziomej
            Vector3 faceMove = stateMachine.MainCameraTransform.forward;
            faceMove.y = 0f;
            faceMove.Normalize();

            // Pobierz wejœcie z klawiatury i znormalizuj je
            Vector3 inputDirection = new Vector3(stateMachine.InputHandler.MovementValue.x, 0f, stateMachine.InputHandler.MovementValue.y);
            inputDirection.Normalize();

            // Oblicz now¹ rotacjê na podstawie sumy kierunku wejœcia i kierunku patrzenia kamery
            Quaternion targetRotation = Quaternion.LookRotation(faceMove, Vector3.up) * Quaternion.LookRotation(inputDirection, Vector3.up);

            // Interpolacja pomiêdzy oryginaln¹ a docelow¹ rotacj¹ z u¿yciem RotationDamping
            Quaternion newRotation = Quaternion.Lerp(originalRotation, targetRotation, stateMachine.RotationDamping);

            // Zastosuj now¹ rotacjê do postaci
            stateMachine.transform.rotation = newRotation;
        }

    }


}
