using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core;
using Base.Data;

namespace Feature.Offer
{
    [Serializable]
    public class OfferTable : TableData<int, OfferEntity>
    {
        public override void Fetch() {
            BG_Offer.ForEachEntity(Get);
        }

        private void Get(BG_Offer entity) {
            var offer = new OfferEntity(entity);
            Dictionary.Add(offer.id, offer);
        }
        
        public List<OfferEntity> GetOffersByType(EResourceOffer type) {
            return Dictionary.Values.Where(offer => offer.resourceType == type).ToList();
        }
    }
}