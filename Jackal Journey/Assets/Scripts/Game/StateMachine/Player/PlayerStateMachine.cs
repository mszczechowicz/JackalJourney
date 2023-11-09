
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//IPLAYER DATA DO WPROWADZENIA PLATER PREFS, NIE IWME CZY BEDZIE TO U�YWANE
public interface IPlayerData
{ 
    Vector3 GetPosition();
    void SetPosition(Vector3 position);

    float GetHealth();
    void SetHealth(float health);


}
//__________________________________________________________-

public class PlayerStateMachine : StateMachine , IPlayerData
{

    [field: SerializeField] public InputHandler InputHandler { get; private set; }

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public InteractionHandler InteractionHandler { get; private set; }
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

    [field: SerializeField] public csHomebrewIK IK { get; private set; }




    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {   //--ImpactStateLogic komentuj� do czas a� zaimplementujemy "HeavyAttack dla boss�w"
        //Health.OnTakeDamage += HandleTakeDamage;
        //---------------------------------------------------------------------------------
        Health.OnDie += HandleIsDead;
    }

    private void OnDisable()
    {
        //--ImpactStateLogic komentuj� do czas a� zaimplementujemy "HeavyAttack dla boss�w"
        //Health.OnTakeDamage -= HandleTakeDamage;
        //---------------------------------------------------------------------------------
        Health.OnDie -= HandleIsDead;
    }

    //--ImpactStateLogic komentuj� do czas a� zaimplementujemy "HeavyAttack dla boss�w"
    //private void HandleTakeDamage()
    //{
    //    SwitchState(new PlayerImpactState(this));
    //}
    //------------------------------------------------------------------------------------
    private void HandleIsDead()
    {
        SwitchState(new PlayerDeadState(this));
    }
    //plAYER PREFS PRAWDOPODOBNIE NIE BEDZIE U�WYANE
    #region PlayerData
    public Vector3 GetPosition()
    {
        return this.transform.position;
      
    }

    public void SetPosition(Vector3 position)
    {
        CharacterController.enabled = false;
        ForceReceiver.enabled = false;
        transform.position = position;
        Debug.Log("Pozycja zmieniona");
        SwitchState(new PlayerFreeLookState(this));
        CharacterController.enabled = true;
        ForceReceiver.enabled= true;

    }

    public float GetHealth()
    {
        return Health.health;
    }

    public void SetHealth(float health)
    {
        Health.SetHealth((int)health);
    }
    #endregion
}
