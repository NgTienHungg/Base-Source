using Base.LoadScene;
using Base;
using Cysharp.Threading.Tasks;

namespace Logic
{
    public class UIButtonReloadScene : UIButtonBase
    {
        protected override void OnClick()
        {
            PanelManager.Instance.LastPanel.SetInteractable(false);
            SceneLoader.Instance.LoadScene(GameConfig.Address.MainScene).Forget();
            SceneLoader.Instance.OnSceneLoaded += async () => await SceneLoader.Instance.ActivateScene();
        }
    }
}