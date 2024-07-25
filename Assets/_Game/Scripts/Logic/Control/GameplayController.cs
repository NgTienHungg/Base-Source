using Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic
{
    public class GameplayController : MonoBehaviour
    {
        private void Start()
        {
            PanelManager.Instance.Create<UIPlayPanel>(GameConfig.Address.UIPlayPanel)
                .ContinueWith(panel => panel.Show().Forget());
        }
    }
}