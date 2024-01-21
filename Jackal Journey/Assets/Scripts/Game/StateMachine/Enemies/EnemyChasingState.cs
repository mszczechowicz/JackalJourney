using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
       


        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackingRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }


        MoveToPlayer(deltaTime);
        

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }
    public override void Exit()
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.ResetPath();
        }
        stateMachine.Agent.velocity = Vector3.zero;
    }

    //Old withoud rotation to PLayer whhen chasing
    //private void MoveToPlayer(float deltatime)
    //{
    //    if (stateMachine.Agent.isOnNavMesh)
    //    {
    //        stateMachine.Agent.destination = stateMachine.Player.transform.position;

    //        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltatime);
    //    }


    //    stateMachine.Agent.nextPosition = stateMachine.transform.position;
    //    stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
    //}
    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            Vector3 desiredDirection = stateMachine.Agent.desiredVelocity.normalized;
            stateMachine.Agent.destination = stateMachine.Player.transform.position;


            if (desiredDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
                stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.RotationSpeed * deltaTime);
            }

            Move(desiredDirection * stateMachine.MovementSpeed, deltaTime);
        }

        stateMachine.Agent.nextPosition = stateMachine.transform.position;
        stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
    }



    private bool IsInAttackingRange()
    {
        if (stateMachine.Player.IsDead){return false;}

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;


        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
