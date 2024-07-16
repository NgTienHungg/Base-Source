using Feature.Resource;
using UnityEngine;

namespace Feature.Offer
{
    public class UIGoldOfferItem : UIResourceOfferItem
    {
        [SerializeField]
        private UIResourceValue price;

        public override void Setup(OfferEntity offer)
        {
            base.Setup(offer);
            price.SetValue(new ResourceValue(EResource.Gem, offer.price));
        }
    }
}