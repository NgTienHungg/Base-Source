using System.Collections.Generic;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using UnityEngine;

#if GAME_FEATURE
using Feature.Offer;
using Feature.Resource;
#endif

namespace Base.Data
{
    public class DatasaveManager : MonoBehaviour
    {
#if GAME_FEATURE
        public OfferSave Offer;
        public ResourceSave Resource;
#endif

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
        }

        private void LoadData() {
#if GAME_FEATURE
            Offer = SaveGame.Load("Offer", new OfferSave("Save"));
            Resource = SaveGame.Load("Resource", new ResourceSave("Save"));

            datasave.Add(Offer);
            datasave.Add(Resource);
#endif
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