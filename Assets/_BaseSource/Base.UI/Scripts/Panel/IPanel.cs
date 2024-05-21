using System;
using Cysharp.Threading.Tasks;

namespace Base.UI
{
    public interface IPanel
    {
        Action OnInit { get; set; }
        Action OnRelease { get; set; }
        Action OnPreOpen { get; set; }
        Action OnPostOpen { get; set; }
        Action OnPreClose { get; set; }
        Action OnPostClose { get; set; }

        bool CanBack { get; }

        void Init();
        UniTask Show();
        UniTask Hide();
        UniTask ShowTween();
        UniTask HideTween();
    }
}