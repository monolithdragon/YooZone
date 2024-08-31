using System;

namespace UnityX {
    public class FuncPredicate(Func<bool> predicate) : IPredicate {
        private readonly Func<bool> predicate = predicate;

        public bool Evaluate() => predicate.Invoke();
    }
}
