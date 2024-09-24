using UnityEngine;

namespace UnityX.SpawnSystem {
    public interface ISpawnPointStrategy {
        Transform NextSpawnPoint();
    }
}
