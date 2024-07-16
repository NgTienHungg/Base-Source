using Base.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Shop
{
    public class UIShopPanel : UIPanel
    {
        [SerializeField]
        private UITabControl tabControl;

        public override bool CanBack => false;

        public override async UniTask Init()
        {
            await base.Init();
            await tabControl.Init();
        }
    }
}