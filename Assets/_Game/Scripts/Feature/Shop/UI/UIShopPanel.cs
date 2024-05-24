using Base.UI;
using UnityEngine;

namespace Feature.Shop
{
    public class UIShopPanel : UIPanel
    {
        [SerializeField]
        private UITabControl tabControl;

        public override bool CanBack => false;

        public override void Init() {
            base.Init();

            // chờ cho ResourcePage sinh ra các Item
            tabControl.Init();

            // get lại các Tween mới sinh ra
            tweenPlayer.Init();
        }

        // private void OnEnable() {
        //     HelloAfter3s();
        // }
        //
        // public async void HelloAfter3s() {
        //     try {
        //         Debug.Log("Prepare for hello...".Color("orange"));
        //         await UniTask.Delay(3000, cancellationToken: tokenSource.Token);
        //         Debug.Log($"Hello {tabControl.gameObject.name}!".Color("cyan"));
        //     }
        //     catch (OperationCanceledException) {
        //         Debug.Log($"Hello {tabControl.gameObject.name}!".Color("cyan"));
        //     }
        // }
    }
}