using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace Base.LoadAsset
{
    public partial class AssetLoader
    {
        public async UniTask<Sprite> LoadSprite(string atlasPath, string spriteName)
        {
            try
            {
                var atlas = await LoadAddressAsync<SpriteAtlas>(atlasPath);
                return atlas.GetSprite(spriteName);
            }
            catch (Exception)
            {
                Debug.LogError($"Not found sprite: {spriteName.Color("red")} in atlas: {atlasPath.Color("red")}");
                return null;
            }
        }
    }
}