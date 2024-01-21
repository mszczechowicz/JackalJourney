using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }
    
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        //FacePlayer();

        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);

        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyIdleWaitState(stateMachine));
        }
    }
    public override void Exit()
    {
     
    }

  

   
}
