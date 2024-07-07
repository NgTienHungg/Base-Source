using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Base.LoadAsset
{
    public class ResourceLoader : IAssetLoader
    {
        private List<Object> listAsset;

        public void Init()
        {
            listAsset = new List<Object>();
        }

        public TAsset Load<TAsset>(string address) where TAsset : Object
        {
            var result = Resources.Load<TAsset>(address);
            listAsset.Add(result);
            return result;
        }

        public async UniTask<TAsset> LoadAsync<TAsset>(string address) where TAsset : Object
        {
            var result = await Resources.LoadAsync<TAsset>(address);
            listAsset.Add(result);
            return (TAsset)result;
        }

        public void ReleaseAll()
        {
            foreach (var asset in listAsset)
            {
                Resources.UnloadAsset(asset);
            }

            listAsset.Clear();
        }
    }
}