using Base.UI;
using Cysharp.Threading.Tasks;

namespace Feature.Shop
{
    public class UIButtonShop : UIButtonBase
    {
        protected override async void OnClick() {
            var playPanel = PanelManager.Instance.LastPanel;
            playPanel.HideTween().Forget();

            var shopPanel = await PanelManager.Instance.CreateAsync<UIShopPanel>(Address.UIShopPanel);
            shopPanel.OnPreClose += () => playPanel.ShowTween().Forget();
            shopPanel.Show().Forget();
        }
    }
}