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

        private void Start() {
            stackPanels.ForEach(p => {
                p.Init();
                p.Show().Forget();
            });
        }

        public async UniTask<TPanel> CreateAsync<TPanel>(string path, Action onLoaded = null) where TPanel : UIPanel {
            var panel = (await Addressables.InstantiateAsync(path, transform)).GetComponent<TPanel>();

            panel.OnInit += () => Register(panel);
            panel.OnRelease += () => Unregister(panel);
            panel.Init();

            Debug.Log($"[PANEL] Created {typeof(TPanel).Name.Color("lime")}");
            onLoaded?.Invoke();

            return panel;
        }

        public async UniTask Close<TPanel>(bool immediately = false) where TPanel : UIPanel {
            var panel = stackPanels.Find(p => p.GetType() == typeof(TPanel));

            if (panel == null) {
                Debug.LogWarning($"[PANEL] Not found {typeof(TPanel).Name.Color("red")}");
                return;
            }

            await panel.Hide();
        }

        private void Register(UIPanel uiPanel) {
            stackPanels.Add(uiPanel);
        }

        private void Unregister(UIPanel uiPanel) {
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