using System;
using Base.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Base.LoadScene
{
    public class SceneLoader : LiveSingleton<SceneLoader>
    {
        [SerializeField]
        private UILoadingScreen loadingScreen;

        [SerializeField]
        private float delayTime = 1.5f;

        public Action<float> OnSceneLoading;
        public Action OnSceneLoaded;
        public Action OnLastSceneHidden;
        public Action OnScenePresented;

        private SceneInstance _scene;
        private bool _isSceneLoaded, _isSceneActivating;
        private float _currentProgress;

        protected override void OnAwake() { }

        public async UniTask LoadScene(string sceneId) {
            Debug.Log($"[SCENE] Load scene: {sceneId.Color("cyan")}");
            InitializeLoading();

            var operationHandle = Addressables.LoadSceneAsync(sceneId, LoadSceneMode.Single, false);
            operationHandle.Completed += OnLoadSceneCompleted;

            await MonitorLoadingProgress(operationHandle);
            OnLastSceneHidden?.Invoke();

            _currentProgress = 0.8f;
            await MonitorSceneActivation();

            await ActivateScene();
        }

        private void InitializeLoading() {
            OnSceneLoading += UpdateLoadingProgress;
            OnScenePresented += loadingScreen.Hide;

            _isSceneLoaded = false;
            loadingScreen.Show();
        }

        private void UpdateLoadingProgress(float progress) {
            loadingScreen.SetProgress(progress);
        }

        private void OnLoadSceneCompleted(AsyncOperationHandle<SceneInstance> handle) {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                _scene = handle.Result;
                _isSceneLoaded = true;
                OnSceneLoaded?.Invoke();
            }
        }

        private async UniTask MonitorLoadingProgress(AsyncOperationHandle<SceneInstance> operationHandle) {
            while (!operationHandle.IsDone) {
                OnSceneLoading?.Invoke(operationHandle.PercentComplete * 0.8f);
                await UniTask.Yield();
            }
        }

        private async UniTask MonitorSceneActivation() {
            while (!_isSceneActivating) {
                _currentProgress = Mathf.Min(0.99f, _currentProgress + 0.01f);
                OnSceneLoading?.Invoke(_currentProgress);
                await UniTask.Delay(250);
            }
        }

        public async UniTask ActivateScene() {
            await UniTask.WaitUntil(() => _isSceneLoaded);
            _isSceneActivating = true;

            var time = 0f;
            var remainingProgress = 1f - _currentProgress;
            while (time < delayTime) {
                OnSceneLoading?.Invoke(_currentProgress + (time / delayTime) * remainingProgress);
                time += Time.fixedDeltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }

            await _scene.ActivateAsync();
            _isSceneLoaded = false;

            await UniTask.Delay(500, ignoreTimeScale: true);
            OnScenePresented?.Invoke();

            ResetActions();
        }

        private void ResetActions() {
            OnSceneLoaded = null;
            OnSceneLoading = null;
            OnScenePresented = null;
            OnLastSceneHidden = null;
        }
    }
}