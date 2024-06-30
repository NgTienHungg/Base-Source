using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    public class UIButtonClosePanel : UIButtonBase
    {
        [SerializeField]
        private UIPanel panel;

        protected override void Reset()
        {
            base.Reset();
            panel = GetComponentInParent<UIPanel>();
        }

        protected override void OnClick()
        {
            panel.Hide().Forget();
        }
    }
}