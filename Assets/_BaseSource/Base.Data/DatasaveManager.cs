using System.Collections.Generic;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using UnityEngine;

namespace Base.Data
{
    public partial class DatasaveManager : MonoBehaviour
    {
        private bool encode = true;
        private string password = "NgTienHung";
        private readonly List<ISaveData> datasave = new List<ISaveData>();

        public void Init() {
#if UNITY_EDITOR
            encode = false;
#endif
            SaveGame.Encode = encode;
            SaveGame.EncodePassword = password;
            SaveGame.Serializer = new SaveGameJsonSerializer();

            LoadData();
            GetData();
        }

        private void SaveData() {
            foreach (var save in datasave) {
                save.Save();
            }
        }

        private void OnApplicationPause(bool pause) {
            if (pause) {
                SaveData();
            }
        }
    }
}