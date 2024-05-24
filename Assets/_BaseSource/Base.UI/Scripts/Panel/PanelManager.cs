using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Base.UI
{
    public class PanelManager : MonoSingleton<PanelManager>
    {
        [ShowInInspector] [ReadOnly]
        private List<UIPanel> stackPanels;

        protected override void OnAwake() {
            stackPanels = GetComponentsInChildren<UIPanel>().ToList();
        }

        private async void Start() {
            await UniTask.WhenAll(stackPanels.Select(p => p.Init()).ToList());
            await UniTask.WhenAll(stackPanels.Select(p => p.PostInit()).ToList());
            stackPanels.ForEach(p => p.Show().Forget());
        }

        public async UniTask<T> CreateAsync<T>(string address) where T : UIPanel {
            var panel = (await Addressables.InstantiateAsync(address, transform)).GetComponent<T>();

            Debug.Log($"[PANEL] Created {typeof(T).Name.Color("lime")}");
            await panel.Init();
            await panel.PostInit();

            return panel;
        }

        public async UniTask<T> TransitionAsync<T>(string address) where T : UIPanel {
            var lastPanel = LastPanel;
            var newPanel = await CreateAsync<T>(address);

            if (lastPanel != null) {
                newPanel.OnPreOpen += () => lastPanel.HideTween().Forget();
                newPanel.OnPreClose += () => lastPanel.ShowTween().Forget();
            }

            await newPanel.Show();
            return newPanel;
        }

        public async UniTask Close<T>(bool immediately = false) where T : UIPanel {
            var panel = stackPanels.Find(p => p.GetType() == typeof(T));

            if (panel == null) {
                Debug.LogWarning($"[PANEL] Not found {typeof(T).Name.Color("red")}");
                return;
            }

            await panel.Hide();
        }

        public void Register(UIPanel uiPanel) {
            stackPanels.Add(uiPanel);
        }

        public void Unregister(UIPanel uiPanel) {
            stackPanels.Remove(uiPanel);
        }

#if UNITY_EDITOR || UNITY_ANDROID

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TryCloseCurrentPanel();
            }
        }

        private void TryCloseCurrentPanel() {
            if (stackPanels.Count == 0 || !stackPanels.Last().CanBack) {
                Debug.Log("[PANEL] Can't back");
                return;
            }

            stackPanels.Last().Hide().Forget();
        }

#endif

        public UIPanel GetPanel<TPanel>() where TPanel : UIPanel {
            var panel = stackPanels.FindAll(p => p.GetType() == typeof(TPanel)).Last();

            if (panel == null) {
                Debug.LogError($"[PANEL] Not found panel {typeof(TPanel).Name.Color("red")}");
                return null;
            }

            return panel;
        }

        public UIPanel LastPanel => stackPanels.Count > 0 ? stackPanels.Last() : null;

        public Type LastPanelType => stackPanels.Count > 0 ? stackPanels.Last().GetType() : null;
    }
}