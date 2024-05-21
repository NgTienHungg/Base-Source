using System;

namespace Feature.Offer
{
    [Serializable]
    public class OfferEntitySave
    {
        public int id;
        public int buyCount;
        public bool isFirstTimeBuy;
        
        public OfferEntitySave(int id) {
            this.id = id;
            buyCount = 0;
            isFirstTimeBuy = true;
        }

        public void Buy() {
            buyCount++;
            isFirstTimeBuy = false;
        }
    }
}