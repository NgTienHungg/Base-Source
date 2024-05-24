using System;
using System.Threading;
using Base.Tween;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    [RequireComponent(typeof(TweenPlayer))]
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public abstract bool CanBack { get; }
        public Action OnPreOpen { get; set; }
        public Action OnPostOpen { get; set; }
        public Action OnPreClose { get; set; }
        public Action OnPostClose { get; set; }

        protected TweenPlayer tweenPlayer;
        protected CancellationTokenSource tokenSource;

        public virtual UniTask Init() {
            PanelManager.Instance.Register(this);
            return UniTask.CompletedTask;
        }

        public virtual async UniTask PostInit() {
            gameObject.SetActive(false);
            tweenPlayer = GetComponent<TweenPlayer>();
            await tweenPlayer.Init();
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

        protected async UniTask Opening() {
            gameObject.SetActive(true);
            await ShowTween();
        }

        protected async UniTask Closing() {
            await HideTween();
            Destroy(gameObject);
        }

        protected virtual void OnDestroy() {
            tokenSource?.Dispose();
            PanelManager.Instance.Unregister(this);
        }
    }
}