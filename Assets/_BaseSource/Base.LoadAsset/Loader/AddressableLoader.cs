using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Base.LoadAsset
{
    public class AddressableLoader : IAssetLoader
    {
        private int nextRequest;

        private readonly Dictionary<int, AsyncOperationHandle> requestDict
            = new Dictionary<int, AsyncOperationHandle>();

        public AssetRequest<TAsset> Load<TAsset>(string address) where TAsset : Object {
            var requestId = nextRequest++;
            var operationHandle = Addressables.LoadAssetAsync<TAsset>(address);
            operationHandle.WaitForCompletion();
            requestDict.Add(requestId, operationHandle);
            var request = new AssetRequest<TAsset>(requestId);
            var setter = (IAssetRequest<TAsset>)request;
            setter.SetTask(UniTask.FromResult(operationHandle.Result));
            setter.SetProgressFunc(() => operationHandle.PercentComplete);
            setter.SetResult(operationHandle.Result);
            setter.SetStatus(operationHandle.Status == AsyncOperationStatus.Succeeded
                ? AssetRequestStatus.Succeeded
                : AssetRequestStatus.Failed);
            setter.SetOperationException(operationHandle.OperationException);
            return request;
        }

        public AssetRequest<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object {
            var requestId = nextRequest++;
            var operationHandle = Addressables.LoadAssetAsync<TAsset>(address);
            requestDict.Add(requestId, operationHandle);

            var request = new AssetRequest<TAsset>(requestId);
            var setter = (IAssetRequest<TAsset>)request;
            var completionSource = new UniTaskCompletionSource<TAsset>();

            setter.SetTask(completionSource.Task);
            setter.SetProgressFunc(() => operationHandle.PercentComplete);
            operationHandle.Completed += x => {
                setter.SetResult(x.Result);
                setter.SetStatus(x.Status == AsyncOperationStatus.Succeeded
                    ? AssetRequestStatus.Succeeded
                    : AssetRequestStatus.Failed);
                setter.SetOperationException(operationHandle.OperationException);
                completionSource.TrySetResult(x.Result);
            };

            return request;
        }

        public void Release(AssetRequest request) {
            if (!requestDict.ContainsKey(request.RequestId)) {
                throw new System.InvalidOperationException(
                    $"There is no asset that has been requested for release (RequestId: {request.RequestId}).");
            }

            var operation = requestDict[request.RequestId];
            Addressables.Release(operation);
            requestDict.Remove(request.RequestId);
        }

        public void ReleaseAll() {
            foreach (var data in requestDict) {
                Addressables.ReleaseInstance(data.Value);
            }

            requestDict.Clear();
        }
    }
}