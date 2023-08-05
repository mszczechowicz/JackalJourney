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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        stateMachine.InteractionHandler.IsInteracting = true;
        stateMachine.GetComponent<CameraMovement>().enabled = false;

    }
    public override void Tick(float deltaTime)
    {

        if (stateMachine.InteractionHandler.IsInteracting == false)
        {
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
