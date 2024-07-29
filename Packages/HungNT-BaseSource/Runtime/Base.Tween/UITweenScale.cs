using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Base
{
    public class UITweenScale : UITweenBase
    {
        [Header("Scale")]
        [SerializeField] private float inactiveScale;
        [SerializeField] private float activeScale = 1f;

        protected override string SettingsPath => "TweenScaleSettings";

        public override async UniTask Show()
        {
            await transform.DOScale(activeScale, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .ToUniTask().ContinueWith(Active);
        }

        public override UniTask Hide()
        {
            return transform.DOScale(inactiveScale, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .ToUniTask().ContinueWith(Inactive);
        }

        protected override void Active()
        {
            transform.localScale = Vector3.one * activeScale;
        }

        protected override void Inactive()
        {
            transform.localScale = Vector3.one * inactiveScale;
        }
    }
}