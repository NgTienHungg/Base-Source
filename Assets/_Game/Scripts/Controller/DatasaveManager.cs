using BayatGames.SaveGameFree;
using Feature.Offer;
using Feature.Resource;

namespace Base.Data
{
    public partial class DatasaveManager
    {
        #region ========== GAME DATASAVE ==========
        public OfferSave Offer;
        public ResourceSave Resource;
        #endregion

        private void LoadData() {
            Offer = SaveGame.Load("Offer", new OfferSave("Save"));
            Resource = SaveGame.Load("Resource", new ResourceSave("Save"));
        }

        private void GetData() {
            datasave.Add(Offer);
            datasave.Add(Resource);
        }
    }
}