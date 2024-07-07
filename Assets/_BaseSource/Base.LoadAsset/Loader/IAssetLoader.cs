using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base.LoadAsset
{
    public interface IAssetLoader
    {
        void Init();
        
        TAsset Load<TAsset>(string address) where TAsset : Object;

        UniTask<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object;

        void ReleaseAll();
    }
}