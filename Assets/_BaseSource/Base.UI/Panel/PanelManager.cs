using System.Collections.Generic;
using System.Linq;
using Base.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Base.UI
{
    public partial class PanelManager : MonoSingleton<PanelManager>
    {
        [ShowInInspector] [ReadOnly]
        private List<UIPanel> stackPanels;

        protected override void OnAwake()
        {
            stackPanels = GetComponentsInChildren<UIPanel>().ToList();
        }

        private async void Start()
        {
            await UniTask.WhenAll(stackPanels.Select(p => p.Init()).ToList());
            await UniTask.WhenAll(stackPanels.Select(p => p.PostInit()).ToList());
            stackPanels.ForEach(p => p.Show().Forget());
        }

        public async UniTask<T> Create<T>(string address) where T : UIPanel
        {
            var panel = (await Addressables.InstantiateAsync(address, transform)).GetComponent<T>();
            await panel.Init();
            await panel.PostInit();
            return panel;
        }

        public async UniTask<T> Show<T>(string address) where T : UIPanel
        {
            var panel = await Create<T>(address);
            await panel.Show();
            return panel;
        }

        public async UniTask Close<T>() where T : UIPanel
        {
            var panel = stackPanels.Find(p => p.GetType() == typeof(T));

            if (panel == null)
            {
                Debug.LogWarning($"[PANEL] Not found {typeof(T).Name.Color("red")}");
                return;
            }

            await panel.Hide();
        }

        public void Register(UIPanel uiPanel)
        {
            stackPanels.Add(uiPanel);
        }

        public void Unregister(UIPanel uiPanel)
        {
            stackPanels.Remove(uiPanel);
        }

#if UNITY_EDITOR || UNITY_ANDROID

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TryCloseCurrentPanel();
            }
        }

        private void TryCloseCurrentPanel()
        {
            if (stackPanels.Count == 0 || !stackPanels.Last().CanBack)
            {
                Debug.Log("[PANEL] Can't back");
                return;
            }

            stackPanels.Last().Hide().Forget();
        }

#endif
    }
}