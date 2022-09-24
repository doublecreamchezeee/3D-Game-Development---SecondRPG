using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        private bool keepSprinting;
        private bool shouldResetSprintState;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }
        #region IState
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

            startTime = Time.time;

            shouldResetSprintState = true;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.SprintParameterHash);
            if (shouldResetSprintState)
            {
                keepSprinting = false;
                stateMachine.ReusableData.ShouldSprint = false;
            }
            
        }


        public override void Update()
        {
            base.Update();
            if (keepSprinting)
            {
                return;
            }

            StopSprinting();
        }

        
        #endregion

        #region Main Methods
        protected void StopSprinting()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardStoppingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion
        #region Reusable Methods

        protected override void OnFall()
        {
            shouldResetSprintState = false;
            base.OnFall();
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            keepSprinting = true;
            stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerform;
            stateMachine.Player.Input.PlayerActions.Sprint.canceled += OnSprintCancel;
        }

        

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerform;
            stateMachine.Player.Input.PlayerActions.Sprint.canceled -= OnSprintCancel;

        }


        #endregion

        #region Input Methods

        private void OnSprintCancel(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardStoppingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingState);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            shouldResetSprintState = false; 
            base.OnJumpStarted(context);

        }

        private void OnSprintPerform(InputAction.CallbackContext context)
        {
            keepSprinting = true;
            stateMachine.ReusableData.ShouldSprint = true;
        }
        #endregion
    }
}
