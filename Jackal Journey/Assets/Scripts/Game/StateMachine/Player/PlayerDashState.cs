using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private readonly int DashHash = Animator.StringToHash("DashingBlendTree");
    private readonly int DashForwardHash = Animator.StringToHash("DashForward");
    private readonly int DashRightHash = Animator.StringToHash("DashRight");

    private const float CrossFadeDuration = 0.1f;

    private float remainingDashTime;

    private Vector3 dashingDirectionInput;
    public PlayerDashState(PlayerStateMachine stateMachine, Vector3 dashingDirectionInput) : base(stateMachine)
    {
        this.dashingDirectionInput = dashingDirectionInput;
    }



    public override void Enter()
    {
        remainingDashTime = stateMachine.DashDuration;
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.IK.enabled = false;
     
        stateMachine.MomentumVFX.Play();
       
     
        stateMachine.Animator.SetFloat(DashForwardHash, dashingDirectionInput.y);
        stateMachine.Animator.SetFloat(DashRightHash, dashingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DashHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
       
            movement += stateMachine.MainCameraTransform.right * dashingDirectionInput.x * stateMachine.DashLength / stateMachine.DashDuration;
            movement += stateMachine.MainCameraTransform.forward * dashingDirectionInput.y * stateMachine.DashLength / stateMachine.DashDuration;

        movement.y = 0f;
        Move(movement, deltaTime);

        FaceTarget();
       


        remainingDashTime -= deltaTime;
        if (remainingDashTime <= 0f)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
            else 
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                return;
            }
        }

       
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
        stateMachine.IK.enabled = true;
        stateMachine.MomentumVFX.Stop();
      

    }

}
