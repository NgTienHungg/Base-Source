using Base;
using Base.Core;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Offer
{
    public class UIResourceOfferItem : MonoBehaviour
    {
        [SerializeField]
        protected Image iconImg;

        [SerializeField]
        protected TextMeshProUGUI valueTxt;

        public OfferEntity Entity => offerEntity;
        protected OfferEntity offerEntity;

        public virtual void Setup(OfferEntity offer)
        {
            offerEntity = offer;

            AssetLoader.Instance.LoadSprite(GameConfig.Address.ShopOfferAtlas, offer.name)
                .ContinueWith(sprite =>
                {
                    iconImg.sprite = sprite;
                    iconImg.SetNativeSize();
                });

            valueTxt.text = offer.value.ToString();
        }
    }
}