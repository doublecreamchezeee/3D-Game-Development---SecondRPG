using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData fallData;

        private Vector3 playerPositionOnEnter;
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airborneData.FallData;
        }
        #region IState
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

            playerPositionOnEnter = stateMachine.Player.transform.position;

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }
        #endregion

        #region Resuable Methods
        protected override void ResetSprintState()
        {
        }

        protected override void OnContactWithGround(Collider collider)
        {
            float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;

            if (fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
            {
                stateMachine.ChangeState(stateMachine.LightLandingState);
                return;
            }
            
            if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardLandingState);
                return; 
            }

            stateMachine.ChangeState(stateMachine.RollingState);
        }
        #endregion

        #region Main Methods
        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalvelocity = GetPlayerVerticalVelocity();

            if (playerVerticalvelocity.y >= -fallData.FallSpeedLimit)
            {
                return;
            }

            Vector3 limitedVelocityForce = new Vector3(0f , -fallData.FallSpeedLimit - playerVerticalvelocity.y, 0f);

            stateMachine.Player.Rigidbody.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
        }

        #endregion
    }
}
