using System;
using Base.LoadScene;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controller
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField]
        private int targetFPS = 60;

        [SerializeField]
        private float delayToActiveNextScene = 2f;

        private async void Start()
        {
            await UniTask.Delay(500);
            await Addressables.InitializeAsync();

            Input.simulateMouseWithTouches = true;
            Application.targetFrameRate = Mathf.Max(targetFPS, (int)Screen.currentResolution.refreshRateRatio.value);

            // loading scene
            SceneLoader.Instance.LoadScene(GameConfig.Address.MainScene).Forget();
            SceneLoader.Instance.OnSceneLoaded += async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delayToActiveNextScene));
                await SceneLoader.Instance.ActivateScene();
            };
        }
    }
}