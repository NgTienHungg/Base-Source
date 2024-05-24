using Base.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        private void Start() {
            PanelManager.Instance.CreateAsync<UIPlayPanel>(Address.UIPlayPanel)
                .ContinueWith(panel => panel.Show().Forget());
        }
    }
}