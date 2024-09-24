using UnityEngine;

namespace UnityX.SpawnSystem {
    public interface IEntityFactory<T> {
        T Create(Transform spawnPoint);
    }
}
