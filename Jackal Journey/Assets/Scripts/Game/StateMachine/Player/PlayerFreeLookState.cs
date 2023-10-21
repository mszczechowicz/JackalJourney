using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

 
public class PlayerFreeLookState : PlayerBaseState
{


    //Stringtohash jest Szybsze w obliczaniu ni� string
    private readonly int VelocityHash = Animator.StringToHash("Velocity");
    

    private const float AnimatorDampTime = 0.1f;


    private readonly int FreeLookHash = Animator.StringToHash("PlayerFreeLookState");


    private const float CrossFadeDuration = 0.1f;

    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;

    public Transform InteractableObject;
    public float InteractionRange = 10f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookHash, CrossFadeDuration);

        stateMachine.InputHandler.JumpEvent += OnJump;
        stateMachine.InputHandler.DashEvent += OnDodge;
    }


    #region Tick
    
    public override void Tick(float deltaTime)
    {
#region Interactions

        if (stateMachine.InputHandler.IsInteracting && stateMachine.InteractionHandler.GetInteractableObject() != null)
        {                 
          
           stateMachine.SwitchState(new PlayerInteractingState(stateMachine));
           return;
        }

#endregion

        if (stateMachine.InputHandler.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (!stateMachine.ForceReceiver.IsGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine, IsMidAirJumped));
            return;
        }


        Vector3 movement = CalculateMovement(deltaTime);




        Move(CalculateSlope(movement) * stateMachine.FreeLookMovementSpeed, deltaTime);






        if (stateMachine.InputHandler.MovementValue == Vector2.zero)
        {

            stateMachine.Animator.SetFloat(VelocityHash, 0, AnimatorDampTime, deltaTime);
            stateMachine.IK.enabled = true;
            return;
        }
        stateMachine.Animator.SetFloat(VelocityHash, 1, AnimatorDampTime, deltaTime);
        stateMachine.IK.enabled = false;


        FaceMovementDirection(movement, deltaTime);





    }
    #endregion
    public override void Exit()
    {

        stateMachine.InputHandler.JumpEvent -= OnJump;
        stateMachine.InputHandler.DashEvent -= OnDodge;

    }
    #region Movement_Data
    private Vector3 CalculateMovement(float deltaTime)
    {

        Vector3 movement = new Vector3();

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // STARY KOD DO DASHOWANIA BEZ U�YCIA DASHA JAKO STANU
        //zostawiam na wszelki wypadek

        //if (remainingDodgeTime > 0f)
        //{

        //    //movement += stateMachine.MainCameraTransform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        //    //movement += stateMachine.MainCameraTransform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        //    movement = forward * stateMachine.InputHandler.MovementValue.y + right * stateMachine.InputHandler.MovementValue.x;
        //    if (movement == Vector3.zero)
        //    {
        //        stateMachine.Animator.Play(DashHash);
        //        stateMachine.ForceReceiver.AddDodgeForce(stateMachine.transform.forward * stateMachine.DodgeForce);
        //    }
        //    else
        //    {
        //        stateMachine.Animator.Play(DodgeHash);
        //        stateMachine.ForceReceiver.AddDodgeForce(stateMachine.transform.forward * stateMachine.DodgeForce);
        //    }


        //    //remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);
        //    //Debug.Log(movement);

        //}
        //else

        movement = forward * stateMachine.InputHandler.MovementValue.y + right * stateMachine.InputHandler.MovementValue.x;

        return movement;



    }
    private void FaceMovementDirection(Vector3 movement, float deltatime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltatime * stateMachine.RotationDamping);
    }

    private Vector3 CalculateSlope(Vector3 movement)
    {
        return Vector3.ProjectOnPlane(movement, stateMachine.ForceReceiver.HitInfo.normal).normalized;
    }
    #endregion
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    #region Dash_Data


    private void OnDodge()
    {
        //if (stateMachine.InputHandler.MovementValue == Vector2.zero)
        //{
        //    return;
        //}
        //else
        //{
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, stateMachine.InputHandler.MovementValue.normalized));
        //}
    }

    #endregion

    
}
