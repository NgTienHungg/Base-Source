using Cysharp.Threading.Tasks;
using Game.DinoPass;
using UnityEngine.AddressableAssets;

namespace Base
{
    public partial class DataConfigManager
    {
        public DinoPassDataConfig DinoPass;

        private async void LoadData()
        {
            Logger.Log("[Data] Load data config...".Color("orange"));
            DinoPass = await Addressables.LoadAssetAsync<DinoPassDataConfig>(GameConfig.Address.DinoPassDataConfig);
        }
    }
}