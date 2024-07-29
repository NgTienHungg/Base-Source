using System;
using Cysharp.Threading.Tasks;

namespace Base
{
    public interface IPanel
    {
        Action OnPreShow { get; set; }
        Action OnPostShow { get; set; }
        Action OnPreHide { get; set; }
        Action OnPostHide { get; set; }

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