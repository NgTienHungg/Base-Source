using BayatGames.SaveGameFree;
using Feature.Offer;
using Feature.Resource;

namespace Base.Data
{
    public partial class DatasaveManager
    {
        public OfferSave Offer;
        public ResourceSave Resource;

        private void LoadData() {
            Offer = SaveGame.Load("Offer", new OfferSave("Save"));
            Resource = SaveGame.Load("Resource", new ResourceSave("Save"));

            datasave.Add(Offer);
            datasave.Add(Resource);
        }
    }
}