using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int TargetLookHash = Animator.StringToHash("PlayerTargetingLookState");
    private readonly int TargetForwardHash = Animator.StringToHash("TargetingForwardVel");
    private readonly int TargetRightHash = Animator.StringToHash("TargetingRightVel");

    

    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        stateMachine.InputHandler.TargetEvent += OnTargetCancel;
        stateMachine.InputHandler.DashEvent += OnDodge;
        stateMachine.Animator.CrossFadeInFixedTime(TargetLookHash, CrossFadeDuration);
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
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }


        Vector3 movement = CalculateMovement(deltaTime);
        Move(CalculateSlope(movement) * stateMachine.TargetingLookMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputHandler.TargetEvent -= OnTargetCancel;
        stateMachine.InputHandler.DashEvent -= OnDodge;
    }

    private void OnTargetCancel()
    {
        stateMachine.Targeter.CancelTargeting();
        UnlockTargetCustomEvent();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement(float deltaTime)
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

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputHandler.MovementValue.y ==0)
        {
           
            stateMachine.Animator.SetFloat(TargetForwardHash, 0,CrossFadeDuration, deltaTime);
            stateMachine.IK.enabled = true;
        }
        else
        {
            float value = stateMachine.InputHandler.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetForwardHash, value, CrossFadeDuration, deltaTime);
            stateMachine.IK.enabled = false;
        }

        if (stateMachine.InputHandler.MovementValue.x == 0)
        {

            stateMachine.Animator.SetFloat(TargetRightHash, 0, CrossFadeDuration, deltaTime);
            stateMachine.IK.enabled = true;
        }
        else
        {
            float value = stateMachine.InputHandler.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetRightHash, value, CrossFadeDuration, deltaTime);
            stateMachine.IK.enabled = false;
        }

    }
    private void OnDodge()
    {
        if (stateMachine.InputHandler.MovementValue == Vector2.zero) { return; }

        stateMachine.SwitchState(new PlayerDashState(stateMachine, stateMachine.InputHandler.MovementValue.normalized));

    }


    private void UnlockTargetCustomEvent()
    {
//#if ENABLE_CLOUD_SERVICES_ANALYTICS
//        int UnLocksTargets = 0; // aktualna liczba œmierci (mo¿esz to pobieraæ z innego miejsca)
//        UnLocksTargets++; // zwiêkszenie liczby œmierci o 1

//        Debug.Log("DeadCounterCustomEvent()");
//        Analytics.CustomEvent("OnTargetLockingEvent", new Dictionary<string, object>
//        {
//            { "UnLocksTargets", UnLocksTargets++ },

//        });
//        AnalyticsService.Instance.CustomData("OnTargetLockingEvent");
//        AnalyticsService.Instance.Flush();
//#endif
    }
}