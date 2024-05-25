using Base.LoadScene;
using Base.UI;
using Cysharp.Threading.Tasks;

namespace Feature.Gameplay
{
    public class UIButtonReloadScene : UIButtonBase
    {
        protected override void OnClick() {
            PanelManager.Instance.LastPanel.SetInteractable(false);
            SceneLoader.Instance.LoadScene(Address.MainScene).Forget();
            SceneLoader.Instance.OnSceneLoaded += async () => await SceneLoader.Instance.ActivateScene();
        }
    }
}