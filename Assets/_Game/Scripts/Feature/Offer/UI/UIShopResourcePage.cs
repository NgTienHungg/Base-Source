using System;
using Base.Data;
using Base.Pool;
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
                var uiGemOffer = PoolManager.Spawn(uiGemOfferPrefab, gemOfferHolder);
                uiGemOffer.Setup(offer);
            }

            var goldOffers = offerTable.GetOffersByType(EResourceOffer.Gold);
            foreach (var offer in goldOffers) {
                var uiGoldOffer = PoolManager.Spawn(uiGoldOfferPrefab, goldOfferHolder);
                uiGoldOffer.Setup(offer);
            }
        }

        private void OnDestroy() {
            PoolManager.DespawnByPrefab(uiGemOfferPrefab);
            PoolManager.DespawnByPrefab(uiGoldOfferPrefab);
        }
    }
}