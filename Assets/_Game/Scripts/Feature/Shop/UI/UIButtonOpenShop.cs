using Base.UI;
using Cysharp.Threading.Tasks;

namespace Feature.Shop
{
    public class UIButtonOpenShop : UIButtonBase
    {
        protected override void OnClick() {
            PanelManager.Instance.TransitionAsync<UIShopPanel>(Address.UIShopPanel).Forget();
        }
    }
}