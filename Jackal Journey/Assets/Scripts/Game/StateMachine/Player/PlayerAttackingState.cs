using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerAttackingState : PlayerBaseState
{

    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private bool alreadyFaceAttack;
    
    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine,int attackIndex) : base(stateMachine)
    {

        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {

        stateMachine.Weapon.SetAttack(attack.Damage,attack.KnockBack);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

        
       
    }

   

    public override void Tick(float deltaTime)
    {
       
        Move(deltaTime);

        CalculateAttackDirection(deltaTime);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if ( normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
              
               
            }


         if (stateMachine.InputHandler.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            
        }


        previousFrameTime = normalizedTime;
    }

    

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));                       
    }

    private void TryApplyForce()
    {   
        if (alreadyAppliedForce)
        {
            return;
        }
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }


    

    private void CalculateAttackDirection(float deltaTime)
    {

        Vector3 faceMove = stateMachine.MainCameraTransform.forward;
        faceMove.y = 0f;
        faceMove.Normalize();      
      
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceMove), deltaTime * stateMachine.RotationDamping);
        
      
    }

}
