using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
public class PlayerImpactState : PlayerBaseState
{
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.1f;

    private float duration = 0.1f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f) { ReturnToLocomotion(); }
    }
    public override void Exit()
    {
        
    }

   

  
}
//-----------------------------------------------------------------------------