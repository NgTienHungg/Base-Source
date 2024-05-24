using Base.UI;
using Cysharp.Threading.Tasks;

namespace Feature.Shop
{
    public class UIButtonShop : UIButtonBase
    {
        protected override async void OnClick() {
            // var playPanel = PanelManager.Instance.LastPanel;
            //
            // var shopPanel = await PanelManager.Instance.CreateAsync<UIShopPanel>(Address.UIShopPanel);
            // shopPanel.OnPreOpen += () => playPanel.HideTween().Forget();
            // shopPanel.OnPreClose += () => playPanel.ShowTween().Forget();
            // shopPanel.Show().Forget();

            await PanelManager.Instance.TransitionAsync<UIShopPanel>(Address.UIShopPanel);
        }
    }
}