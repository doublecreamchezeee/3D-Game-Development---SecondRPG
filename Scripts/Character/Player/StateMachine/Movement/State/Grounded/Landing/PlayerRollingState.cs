using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerRollingState : PlayerLandingState
    {
        private PlayerRollData rollData;
        public PlayerRollingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            rollData = movementData.RollData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            StartAnimation(stateMachine.Player.AnimationData.RollParameterHash);

            stateMachine.ReusableData.MovementSpeedModifier = rollData.SpeedModifier;

            stateMachine.ReusableData.ShouldSprint = false;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.RollParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }
            RotateTowardsTargetRotation();
        }

        public override void OnAnimationsTransitionEvent()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.MediumStoppingState);
                return;
            }

            OnMove();

        }
        #endregion

        #region Input Methods
        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            
        }

        #endregion
    }
}
