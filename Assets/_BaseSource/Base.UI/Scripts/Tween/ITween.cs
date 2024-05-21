using Cysharp.Threading.Tasks;

namespace Base.UI
{
    public interface ITween
    {
        bool IsAutoRun { get; }

        void Init();
        UniTask Show();
        UniTask Hide();
    }
}