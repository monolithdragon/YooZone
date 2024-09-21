using UnityEngine;

namespace UnityX.StateMachine {
    public abstract class State<T>(T controller, Animator animator) : IState {
        protected readonly T controller = controller;
        protected readonly Animator animator = animator;

        protected const float crossFadeDuration = 0.1f;

        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}
