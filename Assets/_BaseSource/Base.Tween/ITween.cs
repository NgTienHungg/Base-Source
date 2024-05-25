using Cysharp.Threading.Tasks;

namespace Base.Tween
{
    public interface ITween
    {
        bool IsAutoRun { get; }

        UniTask Init();
        UniTask Show();
        UniTask Hide();
    }
}