using Cysharp.Threading.Tasks;

namespace Base.UI
{
    public interface ITween
    {
        bool IsAutoRun { get; }

        UniTask Init();
        UniTask Show();
        UniTask Hide();
    }
}