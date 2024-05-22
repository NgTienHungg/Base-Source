using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.Tween
{
    public class UITweenMove : UITweenBase
    {
        [Title("Move")] [SerializeField]
        private RectTransform rectTrans;

        [SerializeField]
        protected Vector2 offset;

        private Vector2 activePos;
        private Vector2 inactivePos;

        protected override string SettingsPath => "TweenMoveSettings";

        protected override void Reset() {
            base.Reset();
            rectTrans = transform as RectTransform;
        }

        protected override void Setup() {
            base.Setup();

            if (rectTrans == null) {
                rectTrans = transform as RectTransform;
            }

            activePos = rectTrans.anchoredPosition;
            inactivePos = activePos + offset;
        }

        public override async UniTask Show() {
            await rectTrans.DOAnchorPos(activePos, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .ToUniTask().ContinueWith(Active);
        }

        public override async UniTask Hide() {
            await rectTrans.DOAnchorPos(inactivePos, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .ToUniTask().ContinueWith(Inactive);
        }

        protected override void Active() {
            rectTrans.anchoredPosition = activePos;
        }

        protected override void Inactive() {
            rectTrans.anchoredPosition = inactivePos;
        }
    }
}