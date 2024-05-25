using System;
using System.Collections.Generic;
using Base.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace Base.LoadAsset
{
    public class AssetLoader : LiveSingleton<AssetLoader>
    {
        [ShowInInspector]
        private readonly Dictionary<Type, Dictionary<string, object>> assetCached
            = new Dictionary<Type, Dictionary<string, object>>();

        [ShowInInspector]
        private readonly Dictionary<Type, Dictionary<string, AssetRequest>> requestCached
            = new Dictionary<Type, Dictionary<string, AssetRequest>>();

        private readonly IAssetLoader addressableLoader = new AddressableLoader();
        private readonly IAssetLoader resourceLoader = new ResourceLoader();

        protected override void OnAwake() {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene) {
            UnloadAll();
        }

        public static async UniTask<T> LoadAsync<T>(string path) where T : Object {
            var type = typeof(T);

            if (!Instance.assetCached.ContainsKey(type)) {
                Instance.assetCached.Add(type, new Dictionary<string, object>());
                Instance.requestCached.Add(type, new Dictionary<string, AssetRequest>());
            }

            var assetsOfType = Instance.assetCached[type];
            var requestsOfType = Instance.requestCached[type];

            if (assetsOfType.ContainsKey(path)) {
                return (T)assetsOfType[path];
            }

            var request = Instance.addressableLoader.LoadAsync<T>(path);
            await request.Task;

            if (!assetsOfType.ContainsKey(path)) {
                assetsOfType.Add(path, request.Result);
                requestsOfType.Add(path, request);
            }

            return request.Result;
        }

        public static T LoadResource<T>(string path) where T : Object {
            var type = typeof(T);

            if (!Instance.assetCached.ContainsKey(type)) {
                Instance.assetCached.Add(type, new Dictionary<string, object>());
                Instance.requestCached.Add(type, new Dictionary<string, AssetRequest>());
            }

            var assetsOfType = Instance.assetCached[type];
            var requestsOfType = Instance.requestCached[type];

            if (assetsOfType.ContainsKey(path)) {
                return (T)assetsOfType[path];
            }

            var request = Instance.resourceLoader.Load<T>(path);

            if (!assetsOfType.ContainsKey(path)) {
                assetsOfType.Add(path, request.Result);
                requestsOfType.Add(path, request);
            }

            return request.Result;
        }

        public static async UniTask<Sprite> LoadSprite(string atlasPath, string spriteName) {
            try {
                var atlas = await LoadAsync<SpriteAtlas>(atlasPath);
                return atlas.GetSprite(spriteName);
            }
            catch (Exception) {
                Debug.LogError($"Not found sprite: {spriteName.Color("red")} in atlas: {atlasPath.Color("red")}");
                return null;
            }
        }

        public static void Unload<T>(string path) where T : Object {
            var type = typeof(T);

            if (!Instance.requestCached.ContainsKey(type))
                return;

            var assetsOfType = Instance.assetCached[type];
            var requestsOfType = Instance.requestCached[type];

            if (assetsOfType.ContainsKey(path)) {
                var request = requestsOfType[path];
                Instance.addressableLoader.Release(request);
                assetsOfType.Remove(path);
                requestsOfType.Remove(path);
            }
        }

        private void UnloadAll() {
            addressableLoader.ReleaseAll();
            assetCached.Clear();
            requestCached.Clear();
        }
    }
}