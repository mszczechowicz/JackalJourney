using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int DeadHash = Animator.StringToHash("Dead");    
    private readonly int IdleHash = Animator.StringToHash("Idle");
    private const float CrossFadeDuration = 1f;

    
    public override void Enter()
    {
        //DeadCounterCustomEvent();
        stateMachine.Animator.CrossFadeInFixedTime(DeadHash, CrossFadeDuration);
        stateMachine.Weapon.gameObject.SetActive(false);
        stateMachine.Health.onDie_UnityEvent.AddListener(Respawn);
       
    }

    public override void Tick(float deltaTime) { }
    
    public override void Exit()
    {
        stateMachine.Health.healthPoints += 100;
        stateMachine.Weapon.gameObject.SetActive(true);
        stateMachine.uiHandler.enabled = true;
       
    }

    private IEnumerator RespawnRoutine()
    {

        Debug.Log("RespawnRoutine()");
        yield return new WaitForSeconds(stateMachine.respawnDelay);       
        yield return stateMachine.fader.FadeOut(stateMachine.fadeTime);    
        stateMachine.CharacterController.enabled = false;
        stateMachine.ForceReceiver.enabled = false;
        stateMachine.transform.position = stateMachine.respawnLocation.position;     
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, CrossFadeDuration);
        stateMachine.CharacterController.enabled = true;
        stateMachine.ForceReceiver.enabled = true;
        stateMachine.Health.healthPoints += 100;
        yield return new WaitForSeconds(stateMachine.respawnDelay);
        yield return stateMachine.fader.FadeIn(stateMachine.fadeTime);
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        Debug.Log(" stateMachine.InputHandler.enabled = true;");
        
    }
    private void Respawn()
    {
        Debug.Log("Respawn()");
        stateMachine.StartCoroutine(RespawnRoutine());
    }

//    private void DeadCounterCustomEvent()
//    {

//#if ENABLE_CLOUD_SERVICES_ANALYTICS
//        int currentDeathCount = 1; // aktualna liczba œmierci (mo¿esz to pobieraæ z innego miejsca)
//        currentDeathCount++; // zwiêkszenie liczby œmierci o 1


//        Debug.Log("DeadCounterCustomEvent()");
//        Analytics.CustomEvent("DeathCounter_Player", new Dictionary<string, object>
//        {
//            { "DeathCount", +1 },

//        });
//        AnalyticsService.Instance.CustomData("DeathCounter_Player");
//        AnalyticsService.Instance.Flush();
//#endif
//    }
}
