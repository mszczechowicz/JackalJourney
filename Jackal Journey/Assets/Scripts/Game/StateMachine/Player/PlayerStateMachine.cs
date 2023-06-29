using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [field: SerializeField] public InputHandler InputHandler { get; private set; }

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    [field: SerializeField] public float RotationDamping { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float MidAirJumpForce { get; private set; }


    [field: SerializeField] public float DodgeForce { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }

    [field: SerializeField] public float DodgeLength { get; private set; }

  
    [field: SerializeField] public float AirMovementSpeed { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;



    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    




    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {   //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
        //Health.OnTakeDamage += HandleTakeDamage;
        //---------------------------------------------------------------------------------
        Health.OnDie += HandleIsDead;
    }

    private void OnDisable()
    {
        //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
        //Health.OnTakeDamage -= HandleTakeDamage;
        //---------------------------------------------------------------------------------
        Health.OnDie -= HandleIsDead;
    }

    //--ImpactStateLogic komentujê do czas a¿ zaimplementujemy "HeavyAttack dla bossów"
    //private void HandleTakeDamage()
    //{
    //    SwitchState(new PlayerImpactState(this));
    //}
    //------------------------------------------------------------------------------------
    private void HandleIsDead()
    {
        SwitchState(new PlayerDeadState(this));
    }















   
   
}
