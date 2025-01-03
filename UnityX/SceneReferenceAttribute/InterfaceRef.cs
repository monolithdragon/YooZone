using System;
using UnityEngine;

namespace UnityX.SceneReferenceAttribute {
    /// <summary>
    /// Allows for serializing Interface types with [SceneRef] attributes.
    /// </summary>
    /// <typeparam name="T">Component type to find and serialize.</typeparam>
    [Serializable]
    public class InterfaceRef<T> : ISerializableRef<T> where T : class {
        [SerializeField] private Component implementer;

        private bool _hasCast;
        private T _value;

        /// <summary>
        /// The serialized interface value.
        /// </summary>
        public T Value {
            get {
                if (!_hasCast) {
                    _hasCast = true;
                    _value = implementer as T;
                }
                return _value;
            }
        }

        object ISerializableRef.SerializedObject
            => implementer;

        public Type RefType => typeof(T);

        public bool HasSerializedObject => implementer != null;

        bool ISerializableRef.OnSerialize(object value) {
            Component c = (Component)value;
            if (c == implementer)
                return false;

            _hasCast = false;
            _value = null;
            implementer = c;
            return true;
        }

        void ISerializableRef.Clear() {
            _hasCast = false;
            _value = null;
            implementer = null;
        }
    }
}
