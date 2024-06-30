using Feature.Offer;

namespace Base.Data
{
    public partial class DatabaseManager
    {
        public OfferTable Offer = new OfferTable();

        private void CollectTables() {
            database.Clear();
            database.Add(Offer);
        }
    }
}