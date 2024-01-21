using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleWaitState : EnemyBaseState
{
    public EnemyIdleWaitState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int WaitHash = Animator.StringToHash("Waiting");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(WaitHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }      
    }
    public override void Exit()
    {

    }

}
