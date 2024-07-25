using BayatGames.SaveGameFree;

namespace Base
{
    public abstract class DataSaveBase
    {
        private readonly string keySave;

        protected DataSaveBase(string keySave)
        {
            this.keySave = keySave;
        }

        public virtual void Save()
        {
            if (keySave.Equals("NotSave"))
                return;

            SaveGame.Save(keySave, this);
        }
    }
}