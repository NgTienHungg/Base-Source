// using UnityEngine;
// using Sirenix.Utilities;
// using System.Collections.Generic;
// using Base.Core;
// using Cysharp.Threading.Tasks;
// using Sirenix.OdinInspector;
// using UnityEngine.AddressableAssets;
//
// namespace Base.Pool
// {
//     public class PoolManager : LiveSingleton<PoolManager>
//     {
//         private readonly Dictionary<string, List<GameObject>> _poolTable
//             = new Dictionary<string, List<GameObject>>();
//
//         private PoolDataSO _poolData;
//
//         protected override void OnAwake() {
//             // load data "PoolDataSO" from Resources & init pools
//             _poolData = Resources.Load<PoolDataSO>(nameof(PoolDataSO));
//
//             _poolData.prefabs.ForEach(
//                 prefab => _poolTable.Add(prefab.name, new List<GameObject>())
//             );
//         }
//
//         public async UniTask<GameObject> Spawn(string prefabName) {
//             // check if pool not contains prefabName
//             if (!_poolTable.ContainsKey(prefabName)) {
//                 Debug.LogError($"[POOL] Instance with name + {prefabName.Color("red")} is not exist in pool!");
//                 return null;
//             }
//
//             // find object not active in hierarchy and is not child of this (PoolManager)
//             var goInactive = _poolTable[prefabName].Find(
//                 go => go.transform.parent == transform && !go.activeInHierarchy
//             );
//             if (goInactive != null) {
//                 goInactive.SetActive(true);
//                 return goInactive;
//             }
//
//             //TODO: create new object, expand list object
//             var newGo = await Addressables.InstantiateAsync(prefabName);
//             _poolTable[prefabName].Add(newGo);
//             newGo.SetActive(true);
//             return newGo;
//         }
//
//         public async UniTask<GameObject> Spawn(string prefabName, Transform parent) {
//             var go = await Spawn(prefabName);
//             if (go != null) go.transform.SetParent(parent);
//             return go;
//         }
//
//         public async UniTask<GameObject> Spawn(string prefabName, Vector3 position, Quaternion rotation) {
//             var go = await Spawn(prefabName);
//             if (go != null) {
//                 go.transform.position = position;
//                 go.transform.rotation = rotation;
//             }
//
//             return go;
//         }
//
//         public async UniTask<GameObject> Spawn(string prefabName, Vector3 position, Quaternion rotation, Transform parent) {
//             var go = await Spawn(prefabName);
//             if (go != null) {
//                 go.transform.SetParent(parent);
//                 go.transform.position = position;
//                 go.transform.rotation = rotation;
//             }
//
//             return go;
//         }
//
//         public void Despawn(GameObject go) {
//             go.SetActive(false);
//             go.transform.SetParent(transform);
//             go.transform.localScale = Vector3.one;
//             go.transform.localPosition = Vector3.zero;
//
//             if (go.TryGetComponent(out ISpawnable despawnable))
//                 despawnable.OnDespawn();
//         }
//
//         public void DespawnAll() {
//             Debug.Log("[POOL] recall all game objects!".Color("orange"));
//             _poolTable.Values.ForEach(listGo => listGo.ForEach(Despawn));
//         }
//
//         public GameObject GetPrefabByName(string prefabName) {
//             return _poolData.prefabs.Find(prefab => prefab.name.Equals(prefabName));
//         }
//
// #if UNITY_EDITOR
//         [Button(ButtonSizes.Medium)]
//         private void OpenPoolDataSO() {
//             UnityEditor.Selection.activeObject = Resources.Load<PoolDataSO>(nameof(PoolDataSO));
//         }
// #endif
//     }
// }