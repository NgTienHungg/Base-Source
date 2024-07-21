using Base;
using Base.Pool;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Offer
{
    public class UIShopResourcePage : UITabPage
    {
        [Header("Holder")]
        [SerializeField] private Transform gemOfferHolder;
        [SerializeField] private Transform goldOfferHolder;

        [Header("Prefabs")]
        [SerializeField] private GameObject uiGemOfferPrefab;
        [SerializeField] private GameObject uiGoldOfferPrefab;

        public override async UniTask Init()
        {
            await base.Init();

            var offerTable = DataManager.Database.Offer;
            var gemOffers = offerTable.GetOffersByType(EResourceOffer.Gem);
            var goldOffers = offerTable.GetOffersByType(EResourceOffer.Gold);

            // uiGemOfferPrefab = await AssetLoader.Instance.LoadAddressAsync<GameObject>(GameConfig.Address.UIShopResourceOffer_Gem);
            // uiGoldOfferPrefab = await AssetLoader.Instance.LoadAddressAsync<GameObject>(GameConfig.Address.UIShopResourceOffer_Gold);

            foreach (var offer in gemOffers)
            {
                var uiGemOffer = PoolManager.Spawn<UIResourceOfferItem>(uiGemOfferPrefab, gemOfferHolder);
                uiGemOffer.Setup(offer);
            }

            foreach (var offer in goldOffers)
            {
                var uiGoldOffer = PoolManager.Spawn<UIResourceOfferItem>(uiGoldOfferPrefab, goldOfferHolder);
                uiGoldOffer.Setup(offer);
            }
        }

        private void OnDestroy()
        {
            PoolManager.DespawnByPrefab(uiGemOfferPrefab);
            PoolManager.DespawnByPrefab(uiGoldOfferPrefab);
        }
    }
}