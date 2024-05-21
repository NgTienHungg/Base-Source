using System;
using BayatGames.SaveGameFree;
using Sirenix.OdinInspector;

namespace Base.Data
{
    [Serializable]
    public abstract class SaveData : ISaveData
    {
        private readonly string keySave;

        protected SaveData(string keySave) {
            this.keySave = keySave;
        }

        [Button]
        public virtual void Save() {
            if (keySave.Equals("NotSave"))
                return;

            SaveGame.Save(keySave, this);
        }

        public virtual void Fix() { }

        public virtual void OnLoaded() { }
    }
}