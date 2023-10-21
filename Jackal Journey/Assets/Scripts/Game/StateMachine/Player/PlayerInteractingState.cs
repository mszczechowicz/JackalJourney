using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractingState : PlayerBaseState
{
    public PlayerInteractingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int InteractionIdleHash = Animator.StringToHash("InteractionIdle");


    private const float CrossFadeDuration = 0.2f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(InteractionIdleHash, CrossFadeDuration);
        //CZY KURSOR MA BYÆ WIDOCZNY PRZY WCHODZENIU W INTERAKCJÊ? A JEŒLI TAK TO DLA JAKICH INTERAKCJI.
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
    //____________________________________________________________________________
        stateMachine.InteractionHandler.IsInteracting = true;
        stateMachine.GetComponent<CameraMovement>().enabled = false;

    }
    public override void Tick(float deltaTime)
    {
        //Wyrzuci³em te linie z freelookstatu aby interakcja odby³¹ siê podczas interaction state
        stateMachine.InteractionHandler.GetInteractableObject().Interact();
        //-----------------
        if (stateMachine.InteractionHandler.IsInteracting == false)
        {

            Debug.Log("KONIEC INTERAKCJI");

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

    }
    public override void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stateMachine.InteractionHandler.IsInteracting = false;
        stateMachine.GetComponent<CameraMovement>().enabled = true;
    }

   
}
