﻿using UnityEngine;

namespace UnityX.SceneReferenceAttribute {
    public class ValidatedMonoBehaviour : MonoBehaviour {
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            this.ValidateRefs();
        }
#else
        protected virtual void OnValidate() { }
#endif
    }
}
