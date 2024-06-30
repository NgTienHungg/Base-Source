using System;
using System.Collections.Generic;
using Base.Data;

namespace Feature.Offer
{
    [Serializable]
    public class OfferSave : SaveData
    {
        public List<OfferEntitySave> offers;

        public OfferSave(string keySave) : base(keySave) {
            var table = DataManager.Database.Offer;

            offers = new List<OfferEntitySave>();
            foreach (var entity in table.Dictionary.Values) {
                var entitySave = new OfferEntitySave(entity.id);
                offers.Add(entitySave);
            }
        }

        public OfferEntitySave Get(int id) {
            return offers.Find(x => x.id == id);
        }
    }
}