using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    protected bool IsMidAirJumped;


    public PlayerBaseState (PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.IsMidAirJumped = false;

    }


    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);

    }

    protected void Move(Vector3 motion, float deltaTime)
    {

        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void ReturnToLocomotion()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    



}
