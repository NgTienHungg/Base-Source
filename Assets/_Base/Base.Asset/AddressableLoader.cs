using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Base
{
    public class AddressableLoader : IAssetLoader
    {
        private List<AsyncOperationHandle> listRequest;

        public void Init()
        {
            listRequest = new List<AsyncOperationHandle>();
        }

        public TAsset Load<TAsset>(string address) where TAsset : Object
        {
            Debug.LogError("AddressLoader does not support Load(). Use ResourceLoader instead.");
            return null;
        }

        public async UniTask<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object
        {
            var requestHandle = Addressables.LoadAssetAsync<TAsset>(address);
            listRequest.Add(requestHandle);

            await requestHandle.Task;
            return requestHandle.Result;
        }

        public void ReleaseAll()
        {
            foreach (var request in listRequest)
            {
                Addressables.ReleaseInstance(request);
            }

            listRequest.Clear();
        }
    }
}