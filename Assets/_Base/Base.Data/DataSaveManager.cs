using System.Collections.Generic;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base
{
    public partial class DataSaveManager : MonoBehaviour
    {
        private readonly List<DataSaveBase> _listData = new List<DataSaveBase>();

        private bool encode = true;
        private const string password = "NgTienHung";

        public void Init()
        {
#if UNITY_EDITOR
            encode = false;
#endif
            SaveGame.Encode = encode;
            SaveGame.EncodePassword = password;
            SaveGame.Serializer = new SaveGameJsonSerializer();

            LoadData();
        }

        [Button]
        private void SaveData()
        {
            foreach (var save in _listData)
            {
                save.Save();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveData();
            }
        }
    }
}