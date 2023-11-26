using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine, IJsonSaveable
{
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }

    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public float AttackRange { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }

    [field: SerializeField] public int AttackKnockback { get; private set; }

    public Health Player { get; private set; }


    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;
        if(Health.healthPoints==0)
            SwitchState(new EnemyDeadState(this));
        else
        SwitchState(new EnemyIdleState(this));
    }
    
    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleIsDead;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleIsDead;
    }

    private void HandleTakeDamage()
    { 
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleIsDead()
    {
        SwitchState(new EnemyDeadState(this));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    public JToken CaptureAsJToken()
    {
        return transform.position.ToToken();
    }

    public void RestoreFromJToken(JToken state)
    {
        CharacterController.enabled = false;
        ForceReceiver.enabled = false;
        transform.position = state.ToVector3();
        CharacterController.enabled = true;
        ForceReceiver.enabled = true;
    }
}
