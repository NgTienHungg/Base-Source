using System;

namespace Feature.Offer
{
    [Serializable]
    public class OfferEntity
    {
        public int id;
        public EResourceOffer resourceType;
        public string name;
        public int value;
        public int price;
        
        public OfferEntity(BG_Offer entity) {
            id = entity.f_Id;
            Enum.TryParse(entity.f_ResourceType, out resourceType);
            name = entity.f_Name;
            value = entity.f_Value;
            price = entity.f_Price;
        }
    }
}