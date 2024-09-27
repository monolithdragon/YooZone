using UnityEngine;

namespace UnityX.SpawnSystem {
    public interface IEntityFactory<T> where T : IEntity {
        T Create(Transform spawnPoint);
    }
}
