using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    public partial class PanelManager
    {
        public UIPanel Get<T>() where T : UIPanel
        {
            var panel = stackPanels.FindAll(p => p.GetType() == typeof(T)).Last();

            if (panel == null)
            {
                Debug.LogError($"[PANEL] Not found panel {typeof(T).Name.Color("red")}");
                return null;
            }

            return panel;
        }

        public async UniTask<T> Transition<T>(string address) where T : UIPanel
        {
            // disable interactable of last panel
            var lastPanel = LastPanel;
            lastPanel.SetInteractable(false);

            // setup transition & enable interactable of last panel when new panel is hidden
            var newPanel = await Create<T>(address);
            newPanel.OnPreShow += () => lastPanel.HideTween().Forget();
            newPanel.OnPreHide += () => lastPanel.ShowTween().Forget();
            newPanel.OnPostHide += () => lastPanel.SetInteractable(true);

            await newPanel.Show();
            return newPanel;
        }

        public UIPanel LastPanel => stackPanels.Count > 0 ? stackPanels.Last() : null;

        public Type LastPanelType => stackPanels.Count > 0 ? stackPanels.Last().GetType() : null;
    }
}