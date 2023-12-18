using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

   
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.1f;

    private float duration = 1f;

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
        stateMachine.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_Fresnel", 1);
        

    }
    private float initialFresnelValue = 1.0f;

  
    // Czas, który up³yn¹³ od rozpoczêcia zmniejszania wartoœci _Fresnel
    private float elapsedTime = 0.0f;


    void UpdateFresnel(float startValue, float endValue)
    {
        // Wykorzystanie funkcji Lerp do interpolacji wartoœci
        float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

        // Ustawienie nowej wartoœci _Fresnel
        stateMachine.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_Fresnel", currentValue);

    
    }

    public override void Tick(float deltaTime)
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Wywo³anie funkcji do stopniowego zmniejszania _Fresnel do zera
            UpdateFresnel(initialFresnelValue, 0.0f);
        }

        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }
    public override void Exit()
    {
        stateMachine.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_Fresnel", 0);
    }

}
