using System;
using System.Collections.Generic;
using Base.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace Base.Asset
{
    public class AssetLoader : MonoSingleton<AssetLoader>
    {
        [ShowInInspector]
        private readonly Dictionary<Type, Dictionary<string, object>> objectCached
            = new Dictionary<Type, Dictionary<string, object>>();

        [ShowInInspector]
        private readonly Dictionary<Type, Dictionary<string, AssetRequest>> assetCachedRequest
            = new Dictionary<Type, Dictionary<string, AssetRequest>>();

        [ShowInInspector]
        private readonly IAssetLoader addressableLoader = new AddressableLoader();

        [ShowInInspector]
        private readonly IAssetLoader resourceLoader = new ResourceLoader();

        protected override void OnAwake() { }

        public async UniTask<T> LoadAsync<T>(string path) where T : Object {
            var type = typeof(T);

            if (!objectCached.ContainsKey(type)) {
                objectCached.Add(type, new Dictionary<string, object>());
                assetCachedRequest.Add(type, new Dictionary<string, AssetRequest>());
            }

            var dic = objectCached[type];
            var request = assetCachedRequest[type];
            if (dic.ContainsKey(path)) {
                return (T)dic[path];
            }

            var loader = addressableLoader.LoadAsync<T>(path);

            await loader.Task;
            if (!dic.ContainsKey(path)) {
                request.Add(path, loader);
                dic.Add(path, loader.Result);
            }

            return loader.Result;
        }

        public T LoadResource<T>(string path) where T : Object {
            var type = typeof(T);

            if (!objectCached.ContainsKey(type)) {
                objectCached.Add(type, new Dictionary<string, object>());
                assetCachedRequest.Add(type, new Dictionary<string, AssetRequest>());
            }

            var objectDict = objectCached[type];
            var requestDict = assetCachedRequest[type];
            if (objectDict.ContainsKey(path)) {
                return (T)objectDict[path];
            }

            var request = resourceLoader.Load<T>(path);
            if (!objectDict.ContainsKey(path)) {
                requestDict.Add(path, request);
                objectDict.Add(path, request.Result);
            }

            return request.Result;
        }

        public void UnloadAsset<T>(string name) where T : Object {
            var type = typeof(T);
            if (!assetCachedRequest.ContainsKey(type)) return;
            var requestDict = assetCachedRequest[type];
            if (requestDict.ContainsKey(name)) {
                addressableLoader.Release(requestDict[name]);
                objectCached[type].Remove(name);
                requestDict.Remove(name);
            }
        }

        public static async UniTask<Sprite> LoadSprite(string atlasPath, string spriteName) {
            try {
                var atlas = await Instance.LoadAsync<SpriteAtlas>(atlasPath);
                return atlas.GetSprite(spriteName);
            }
            catch (Exception) {
                Debug.LogError($"Not found sprite: {spriteName.Color("red")} in atlas: {atlasPath.Color("red")}");
                return null;
            }
        }
    }
}