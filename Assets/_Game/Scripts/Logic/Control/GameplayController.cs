using Base.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic
{
    public class GameplayController : MonoBehaviour
    {
        private void Start() {
            PanelManager.Instance.Create<UIPlayPanel>(Address.UIPlayPanel)
                .ContinueWith(panel => panel.Show().Forget());
        }
    }
}