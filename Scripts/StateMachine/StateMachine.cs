using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    public abstract class StateMachine
    {
        protected IState currentState;
        
        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;

            currentState?.Enter();
        }
        
        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationsEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationsExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            currentState?.OnAnimationsTransitionEvent();
        }

        public void OnTriggerEnter(Collider collider)
        {
            currentState?.OnTriggerEnter(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            currentState?.OnTriggerExit(collider);
        }
    }
}
