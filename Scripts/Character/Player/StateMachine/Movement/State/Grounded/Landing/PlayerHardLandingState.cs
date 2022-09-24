using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerHardLandingState : PlayerLandingState
    {
        public PlayerHardLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);

            //stateMachine.Player.Input.PlayerActions.Movement.Disable();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);
            stateMachine.Player.Input.PlayerActions.Movement.Enable();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            ResetVelocity();
        }

        public override void OnAnimationsExitEvent()
        {
            stateMachine.Player.Input.PlayerActions.Movement.Enable(); 
        }

        public override void OnAnimationsTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        #endregion

        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        }

        protected override void OnMove()
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion

        #region Input Methods

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            

        }
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }

        #endregion
    }
}
