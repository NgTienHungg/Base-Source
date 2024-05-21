using Base.Core;
using Base.Data;
using Base.UI;
using UnityEngine;

namespace Feature.Offer
{
    public class UIShopResourcePage : UITabPage
    {
        [Header("Prefab")]
        [SerializeField]
        private UIResourceOfferItem uiGemOfferPrefab;

        [SerializeField]
        private UIResourceOfferItem uiGoldOfferPrefab;

        [Header("Holder")]
        [SerializeField]
        private Transform gemOfferHolder;

        [SerializeField]
        private Transform goldOfferHolder;

        public override void Init() {
            base.Init();

            var offerTable = DataManager.Database.Offer;
            var gemOffers = offerTable.GetOffersByType(EResourceOffer.Gem);
            foreach (var offer in gemOffers) {
                var uiGemOffer = Instantiate(uiGemOfferPrefab, gemOfferHolder);
                uiGemOffer.Setup(offer);
            }

            var goldOffers = offerTable.GetOffersByType(EResourceOffer.Gold);
            foreach (var offer in goldOffers) {
                var uiGoldOffer = Instantiate(uiGoldOfferPrefab, goldOfferHolder);
                uiGoldOffer.Setup(offer);
            }
        }
    }
}