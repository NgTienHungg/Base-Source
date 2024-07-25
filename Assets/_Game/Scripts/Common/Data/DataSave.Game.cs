using BayatGames.SaveGameFree;
using Game.DinoPass;
using Sirenix.OdinInspector;

namespace Base
{
    public partial class DataSaveManager
    {
        [GUIColor("yellow")]
        public DinoPassDataSave DinoPass;

        private void LoadData()
        {
            Logger.Log("[Data] Load data save...".Color("orange"));
            DinoPass = SaveGame.Load(GameConfig.File.DinoPassDataSave, new DinoPassDataSave(GameConfig.File.DinoPassDataSave));
        }
    }
}