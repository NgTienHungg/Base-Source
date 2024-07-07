// using System;
// using Cysharp.Threading.Tasks;
//
// namespace Base.LoadAsset
// {
//     public interface IAssetRequest<TAsset>
//     {
//         void SetTask(UniTask<TAsset> task);
//
//         void SetProgressFunc(Func<float> progress);
//
//         void SetResult(TAsset result);
//
//         void SetStatus(AssetRequestStatus status);
//
//         void SetOperationException(Exception ex);
//     }
// }