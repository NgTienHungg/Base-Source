using Base.Utils;
using Base.Utils.Extension;
using TMPro;
using UnityEngine;

namespace Feature.Offer
{
    public class UIGemOfferItem : UIResourceOfferItem
    {
        [SerializeField]
        private TextMeshProUGUI priceTxt;

        public override void Setup(OfferEntity offer) {
            base.Setup(offer);
            priceTxt.text = offer.price.ToCurrencyFormat();
        }
    }
}