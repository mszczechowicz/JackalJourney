using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int DeadHash = Animator.StringToHash("Dead");
    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DeadHash, CrossFadeDuration);
        stateMachine.Weapon.gameObject.SetActive(false);
       
        stateMachine.Health.onDie_UnityEvent.AddListener(DissolveEnemy);
       
        
        
    }

    public override void Tick(float deltaTime)
    {
    
    }
    public override void Exit()
    {
        
    }

    private void DissolveEnemy()
    {
        stateMachine.StartCoroutine(DissolveRoutine());

    }

    private IEnumerator DissolveRoutine()
    {

        Debug.Log("DisssolveENEMY)");
        //yield return new WaitForSeconds(5f);
        yield return stateMachine.DissolverController.DissolveCo();
        stateMachine.PermamentDead();

    }

   
}

