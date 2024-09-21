using System;

namespace UnityX.StateMachine {
    public class FuncPredicate(Func<bool> predicate) : IPredicate {
        private readonly Func<bool> predicate = predicate;

        public bool Evaluate() => predicate.Invoke();
    }
}
