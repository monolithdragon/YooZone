﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace UnityX.SceneReferenceAttribute {
    [InitializeOnLoad]
    public static class SceneRefValidatorOnSave {
        private const string PrefsKey = "YooZone/ValidateRefsOnSave";
        private const string MenuItemText = "YooZone/Validate Refs on Save";

        public static bool ValidateRefsOnSave {
            get => EditorPrefs.GetBool(PrefsKey, false);
            private set => EditorPrefs.SetBool(PrefsKey, value);
        }

        static SceneRefValidatorOnSave() {
            EditorSceneManager.sceneSaving += OnSceneSaving;
        }

        [MenuItem(MenuItemText, false, 1000)]
        public static void ToggleValidateRefsOnSave() {
            ValidateRefsOnSave = !ValidateRefsOnSave;
            Menu.SetChecked(MenuItemText, ValidateRefsOnSave);
        }

        [MenuItem(MenuItemText, true)]
        public static bool ToggleValidateRefsOnSaveValidate() {
            Menu.SetChecked(MenuItemText, ValidateRefsOnSave);

            return true;
        }

        private static void OnSceneSaving(Scene scene, string path) {
            if (!ValidateRefsOnSave)
                return;

            SceneRefAttributeValidator.ValidateAllRefs();
        }
    }
}
#endif
