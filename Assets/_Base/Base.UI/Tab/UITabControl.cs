using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base
{
    public class UITabControl : MonoBehaviour
    {
        [SerializeField]
        private List<TabGroup> tabs = new List<TabGroup>();

        [Title("Auto Open")]
        [SerializeField] private bool autoInit;
        [SerializeField] private bool autoOpen = true;
        [SerializeField] [ShowIf("@autoOpen")] private int autoOpenTab;

        private void Awake()
        {
            if (autoInit)
            {
                Init();
            }
        }

        public UniTask Init()
        {
            var initTasks = new List<UniTask>();
            for (var i = 0; i < tabs.Count; i++)
            {
                tabs[i].Register(this, i);
                initTasks.Add(tabs[i].Init());
            }

            return UniTask.WhenAll(initTasks);
        }

        private void OnEnable()
        {
            if (autoOpen)
            {
                OpenTab(autoOpenTab);
            }
        }

        public void OpenTab(int index)
        {
            if (index < 0 || index >= tabs.Count)
            {
                Debug.LogWarning("Invalid tab index: " + index);
                index = 0;
            }

            for (var i = 0; i < tabs.Count; i++)
            {
                if (i == index)
                {
                    tabs[i].Show();
                }
                else
                {
                    tabs[i].Hide();
                }
            }
        }
    }
}