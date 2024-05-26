using System;
using Base.UI;
using UnityEngine;

namespace Logic
{
    public class UIButtonTestAction : UIButtonBase
    {
        public int count = 3;

        private void Awake() {
            for (var i = 0; i < count; i++) {
                var x = i;
                button.onClick.AddListener(() => ShowMessage(x));
            }

            void ShowMessage(int x) {
                Debug.Log($"Button {x} clicked at frameCount: {Time.frameCount}".Color("orange"));
            }
        }

        protected override void OnClick() { }
    }
}