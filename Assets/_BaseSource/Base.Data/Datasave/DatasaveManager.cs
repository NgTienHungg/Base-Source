using System.Collections.Generic;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using Sirenix.OdinInspector;
using UnityEngine;
using Feature.Offer;
using Feature.Resource;

namespace Base.Data
{
    public class DatasaveManager : MonoBehaviour
    {
        #region =====>>> GAME <<<=====
        public OfferSave Offer;
        public ResourceSave Resource;
        #endregion

        [ShowInInspector] [ReadOnly]
        private bool encode = true;

        [ShowInInspector] [ReadOnly]
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
        }

        private void LoadData() {
            Offer = SaveGame.Load("Offer", new OfferSave("Save"));
            Resource = SaveGame.Load("Resource", new ResourceSave("Save"));

            datasave.Add(Offer);
            datasave.Add(Resource);
        }

        [Button]
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