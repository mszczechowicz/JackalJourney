using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int DeadHash = Animator.StringToHash("Dead");
    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        UnlockTargetCustomEvent();
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


    private void UnlockTargetCustomEvent()
    {
//#if ENABLE_CLOUD_SERVICES_ANALYTICS
//        int deadCounts = 1; // aktualna liczba œmierci (mo¿esz to pobieraæ z innego miejsca)
//        deadCounts++; // zwiêkszenie liczby œmierci o 1

       
//        Analytics.CustomEvent("CounterKilledEnemies", new Dictionary<string, object>
//        {
//            { "D", deadCounts++ },

//        });
//        AnalyticsService.Instance.CustomData("CounterKilledEnemies");
//        AnalyticsService.Instance.Flush();
//#endif
    }

}

