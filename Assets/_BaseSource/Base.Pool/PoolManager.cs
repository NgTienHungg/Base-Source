using System.Linq;
using Base.Core;
using Lean.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Pool
{
    public class PoolManager : LiveSingleton<PoolManager>
    {
        protected override void OnAwake() {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnDestroy() {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene curScene, Scene nextScene) {
            DespawnAll();
        }

        public static GameObject Spawn(GameObject prefab) {
            return LeanPool.Spawn(prefab);
        }

        public static GameObject Spawn(GameObject prefab, Transform parent) {
            return LeanPool.Spawn(prefab, parent);
        }

        public static T Spawn<T>(GameObject prefab) {
            return LeanPool.Spawn(prefab).GetComponent<T>();
        }

        public static T Spawn<T>(GameObject prefab, Transform parent) {
            return LeanPool.Spawn(prefab, parent).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab) where T : Component {
            return LeanPool.Spawn(prefab);
        }

        public static T Spawn<T>(T prefab, Transform parent) where T : Component {
            return LeanPool.Spawn(prefab, parent);
        }

        public static void Despawn(GameObject clone) {
            LeanPool.Despawn(clone);
        }

        public static void Despawn(Component clone) {
            LeanPool.Despawn(clone);
        }

        public static void DespawnByPrefab(GameObject prefab) {
            foreach (var instance in LeanGameObjectPool.Instances.Where(instance => instance.Prefab == prefab)) {
                instance.DespawnAll(true);
                return;
            }
        }

        public static void DespawnByPrefab(Component prefab) {
            DespawnByPrefab(prefab.gameObject);
        }

        public static void DespawnAll() {
            LeanPool.DespawnAll();
        }
    }
}