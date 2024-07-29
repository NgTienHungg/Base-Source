using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base
{
    [RequireComponent(typeof(TweenPlayer), typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public abstract bool CanBack { get; }
        public Action OnPreShow { get; set; }
        public Action OnPostShow { get; set; }
        public Action OnPreHide { get; set; }
        public Action OnPostHide { get; set; }

        [SerializeField] protected TweenPlayer tweenPlayer;
        [SerializeField] protected CanvasGroup canvasGroup;

        protected CancellationTokenSource tokenSource;

        protected void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayer>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Thực hiện các tác vụ init, sinh, await ... của các thành phần trong panel. 
        /// </summary>
        public virtual UniTask Init()
        {
            PanelManager.Instance.Register(this);
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// Chờ Init() vì trong khi đó có thể sinh ra các item có Tween
        /// Sau đó mới init tween để show đủ tất cả các animation
        /// </summary>
        public virtual async UniTask PostInit()
        {
            gameObject.SetActive(false);
            await tweenPlayer.Init();
        }

        public async UniTask Show()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            OnPreShow?.Invoke();
            gameObject.SetActive(true);
            await ShowTween();
            OnPostShow?.Invoke();
        }

        public async UniTask Hide()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            OnPreHide?.Invoke();
            await HideTween();
            Destroy(gameObject);
            OnPostHide?.Invoke();
        }

        public UniTask ShowTween()
        {
            return tweenPlayer.ShowTween(tokenSource.Token);
        }

        public UniTask HideTween()
        {
            return tweenPlayer.HideTween(tokenSource.Token);
        }

        protected virtual void OnDestroy()
        {
            tokenSource?.Dispose();
            PanelManager.Instance.Unregister(this);
        }

        public void SetInteractable(bool interactable)
        {
            canvasGroup.interactable = interactable;
        }
    }
}