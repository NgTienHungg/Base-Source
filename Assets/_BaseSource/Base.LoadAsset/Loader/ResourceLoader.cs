using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Base.LoadAsset
{
    public class ResourceLoader : IAssetLoader
    {
        private int nextRequestId;

        public AssetRequest<TAsset> Load<TAsset>(string address) where TAsset : Object {
            var requestId = nextRequestId++;
            var request = new AssetRequest<TAsset>(requestId);
            var setter = (IAssetRequest<TAsset>)request;
            var result = Resources.Load<TAsset>(address);

            setter.SetTask(UniTask.FromResult(result));
            setter.SetProgressFunc(() => 1.0f);
            setter.SetResult(result);
            setter.SetStatus(result != null ? AssetRequestStatus.Succeeded : AssetRequestStatus.Failed);
            if (result == null) {
                setter.SetOperationException(
                    new InvalidOperationException($"Requested asset（Key: {address}）was not found.")
                );
            }

            return request;
        }

        public AssetRequest<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object {
            var requestId = nextRequestId++;
            var request = new AssetRequest<TAsset>(requestId);
            var setter = (IAssetRequest<TAsset>)request;
            var completionSource = new UniTaskCompletionSource<TAsset>();

            var resourceRequest = Resources.LoadAsync<TAsset>(address);
            setter.SetTask(completionSource.Task);
            setter.SetProgressFunc(() => resourceRequest.progress);
            resourceRequest.completed += _ => {
                var result = resourceRequest.asset as TAsset;
                setter.SetResult(result);
                setter.SetStatus(result != null ? AssetRequestStatus.Succeeded : AssetRequestStatus.Failed);
                if (result == null) {
                    setter.SetOperationException(
                        new InvalidOperationException($"Requested asset（Key: {address}）was not found.")
                    );
                }

                completionSource.TrySetResult(result);
            };

            return request;
        }

        public void Release(AssetRequest request) { }

        public void ReleaseAll() { }
    }
}