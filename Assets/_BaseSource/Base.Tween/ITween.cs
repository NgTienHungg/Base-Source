using Cysharp.Threading.Tasks;

namespace Base.Tween
{
    public interface ITween
    {
        bool IsAutoRun { get; }

        void Init();
        UniTask Show();
        UniTask Hide();
    }
}