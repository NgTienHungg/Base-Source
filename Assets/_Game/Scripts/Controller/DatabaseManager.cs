using Feature.Offer;

namespace Base.Data
{
    public partial class DatabaseManager
    {
        #region ========== GAME DATABASE ==========
        public OfferTable Offer = new OfferTable();
        #endregion
        
        private void GetTables() {
            database.Clear();
            database.Add(Offer);
        }
    }
}