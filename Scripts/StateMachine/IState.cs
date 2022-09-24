using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    public interface IState 
    {
        public void Enter();

        public void Exit();

        public void HandleInput();

        public void Update();

        public void PhysicsUpdate();

        public void OnAnimationsEnterEvent();

        public void OnAnimationsExitEvent(); 

        public void OnAnimationsTransitionEvent();

        public void OnTriggerEnter(Collider collider);

        public void OnTriggerExit(Collider collider);
    }
}
