using UnityEngine;

namespace UnityX.SpawnSystem {
    public class EntityFactory<T> : IEntityFactory<T> {
        private readonly GameObject _prefab;

        public EntityFactory(GameObject prefab) => _prefab = prefab;

        public T Create(Transform spawnPoint) {
            var entity = GameObject.Instantiate(_prefab, spawnPoint.position, Quaternion.identity);
            return entity.GetComponent<T>();
        }
    }
}
