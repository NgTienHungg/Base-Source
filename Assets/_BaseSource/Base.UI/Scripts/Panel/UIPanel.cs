using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    [RequireComponent(typeof(TweenPlayer))]
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public abstract bool CanBack { get; }
        public Action OnInit { get; set; }
        public Action OnRelease { get; set; }
        public Action OnPreOpen { get; set; }
        public Action OnPostOpen { get; set; }
        public Action OnPreClose { get; set; }
        public Action OnPostClose { get; set; }

        protected TweenPlayer tweenPlayer;
        protected CancellationTokenSource tokenSource;

        public virtual void Init() {
            OnInit?.Invoke();
            tweenPlayer = GetComponent<TweenPlayer>();
            tweenPlayer.Init();
        }

        public async UniTask Show() {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            OnPreOpen?.Invoke();
            await Opening();
            OnPostOpen?.Invoke();
        }

        public async UniTask Hide() {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            OnPreClose?.Invoke();
            await Closing();
            OnPostClose?.Invoke();
        }

        public UniTask ShowTween() {
            return tweenPlayer.ShowTween(tokenSource.Token);
        }

        public UniTask HideTween() {
            return tweenPlayer.HideTween(tokenSource.Token);
        }

        //TODO: can override
        protected virtual async UniTask Opening() {
            gameObject.SetActive(true);
            await ShowTween();
        }

        //TODO: can override
        protected virtual async UniTask Closing() {
            await HideTween();
            Destroy(gameObject);
        }

        protected virtual void OnDestroy() {
            tokenSource?.Dispose();
            OnRelease?.Invoke();
        }
    }
}