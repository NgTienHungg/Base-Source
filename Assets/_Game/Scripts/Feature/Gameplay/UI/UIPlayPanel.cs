using Base.UI;
using Controller;
using TMPro;
using UnityEngine;

namespace Feature.Gameplay
{
    public class UIPlayPanel : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI goldText;

        public override bool CanBack => false;

        private void FixedUpdate() {
            goldText.text = GameManager.Instance.Gold.ToString();
        }
    }
}