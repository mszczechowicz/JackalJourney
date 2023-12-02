using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int TargetLookHash = Animator.StringToHash("PlayerTargetingLookState");


    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        stateMachine.InputHandler.TargetCancelEvent += OnTargetCancel;
        stateMachine.Animator.CrossFadeInFixedTime(TargetLookHash, CrossFadeDuration);
    }



    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(CalculateSlope(movement) * stateMachine.TargetingLookMovementSpeed, deltaTime);
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputHandler.TargetCancelEvent -= OnTargetCancel;
    }

    private void OnTargetCancel()
    {
        stateMachine.Targeter.CancelTargeting();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputHandler.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputHandler.MovementValue.y;

        return movement;


    }
    private Vector3 CalculateSlope(Vector3 movement)
    {
        return Vector3.ProjectOnPlane(movement, stateMachine.ForceReceiver.HitInfo.normal).normalized;
    }
}