using System;
using Cysharp.Threading.Tasks;

namespace Base.UI
{
    public interface IPanel
    {
        Action OnPreOpen { get; set; }
        Action OnPostOpen { get; set; }
        Action OnPreClose { get; set; }
        Action OnPostClose { get; set; }

        bool CanBack { get; }
        void SetInteractable(bool interactable);

        UniTask Init();
        UniTask PostInit();
        UniTask Show();
        UniTask Hide();
        UniTask ShowTween();
        UniTask HideTween();
    }
}