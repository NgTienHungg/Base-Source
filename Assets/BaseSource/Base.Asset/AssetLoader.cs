using System;
using System.Collections.Generic;
using Base.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Base.Asset
{
    public partial class AssetLoader : LiveSingleton<AssetLoader>
    {
        [ShowInInspector]
        private Dictionary<Type, Dictionary<string, object>> assetsCached;

        private IAssetLoader resourceLoader;
        private IAssetLoader addressableLoader;

        protected override void OnAwake()
        {
            assetsCached = new Dictionary<Type, Dictionary<string, object>>();

            resourceLoader = new ResourceLoader();
            resourceLoader.Init();

            addressableLoader = new AddressableLoader();
            addressableLoader.Init();

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private T GetOrAddToCache<T>(string path, Func<string, T> load) where T : Object
        {
            var type = typeof(T);
            if (!assetsCached.TryGetValue(type, out var assetsOfType))
            {
                assetsOfType = new Dictionary<string, object>();
                assetsCached[type] = assetsOfType;
            }

            if (assetsOfType.TryGetValue(path, out var asset))
            {
                return (T)asset;
            }

            var result = load(path);
            assetsOfType[path] = result;
            return result;
        }

        private async UniTask<T> GetOrAddToCacheAsync<T>(string path, Func<string, UniTask<T>> loadAsync) where T : Object
        {
            var type = typeof(T);
            if (!assetsCached.TryGetValue(type, out var assetsOfType))
            {
                assetsOfType = new Dictionary<string, object>();
                assetsCached[type] = assetsOfType;
            }

            if (assetsOfType.TryGetValue(path, out var asset))
            {
                return (T)asset;
            }

            var result = await loadAsync(path);
            assetsOfType[path] = result;
            return result;
        }

        public T LoadResource<T>(string path) where T : Object
        {
            return GetOrAddToCache(path, resourceLoader.Load<T>);
        }

        public UniTask<T> LoadResourceAsync<T>(string path) where T : Object
        {
            return GetOrAddToCacheAsync(path, resourceLoader.LoadAsync<T>);
        }

        public UniTask<T> LoadAddressAsync<T>(string path) where T : Object
        {
            return GetOrAddToCacheAsync(path, addressableLoader.LoadAsync<T>);
        }

        private void UnloadAll()
        {
            resourceLoader.ReleaseAll();
            addressableLoader.ReleaseAll();
            assetsCached.Clear();
        }

        private void OnActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
        {
            UnloadAll();
        }
    }
}