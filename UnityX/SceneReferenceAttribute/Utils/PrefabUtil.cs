using UnityEngine;

namespace UnityX.SceneReferenceAttribute.Utils {
    public class PrefabUtil {
        public static bool IsUninstantiatedPrefab(GameObject obj)
            => obj.scene.rootCount == 0;
    }
}
