using Base.Core;
using Base.UI;
using Cysharp.Threading.Tasks;
using Feature.Gameplay;

namespace Controller
{
    public class GameManager : MonoSingleton<GameManager>
    {
        protected override void OnAwake() { }

        private void Start() {
            PanelManager.Instance.CreateAsync<UIPlayPanel>(Address.UIPlayPanel)
                .ContinueWith(async panel => {
                    await UniTask.Yield();
                    panel.Show().Forget();
                });
        }
    }
}