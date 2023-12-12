
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class PlayerStateMachine : StateMachine, IJsonSaveable
{
    [field: Header("Components")]
    [field: SerializeField] public InputHandler InputHandler { get; private set; }

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public WeaponDamage Weapon { get; private set; }

    [field: SerializeField] public csHomebrewIK IK { get; private set; }

    [field: SerializeField] public InteractionHandler InteractionHandler { get; private set; }
   
    [field: SerializeField] public Targeter Targeter { get; private set; }


    [field: Header("MovementSettings")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingLookMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }


    [field: Header("JumpingSettings")]
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float MidAirJumpForce { get; private set; }

    [field: SerializeField] public float AirMovementSpeed { get; private set; }


    [field: Header("VFXSettings")]
    [field: SerializeField] public ParticleSystem MomentumVFX { get; private set; }


    [field: Header("DodgingSettings")]
   
    
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }

    [field: SerializeField] public float DashForce { get; private set; }
    [field: SerializeField] public float DashDuration { get; private set; }
    [field: SerializeField] public float DashLength { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    [field: Header("CombatSettings")]
    [field: SerializeField] public Attack[] Attacks { get; private set; }

  

    [field: Header("RespawnSettings")]
    [field: SerializeField]public Transform respawnLocation{ get; private set; }
    [field: SerializeField]public float respawnDelay { get; private set; }
    [field: SerializeField]public float fadeTime { get; private set; }
    public Fader fader{ get; set; }
    public UIHandler uiHandler { get; set; }
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        fader = FindObjectOfType<Fader>();
        uiHandler = FindObjectOfType<UIHandler>();
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
