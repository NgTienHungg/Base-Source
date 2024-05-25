using Base.LoadAsset;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Resource
{
    public class UIResourceValue : MonoBehaviour
    {
        [SerializeField]
        private Image iconImg;

        [SerializeField]
        private TextMeshProUGUI valueTxt;

        [SerializeField]
        private ResourceValue resourceValue;

        private void Reset() {
            iconImg = GetComponentInChildren<Image>();
            valueTxt = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void OnEnable() {
            SetupUI();
        }

        private void SetupUI() {
            AssetLoader.LoadSprite(Address.ResourceAtlas, resourceValue.resourceType.ToString())
                .ContinueWith(sprite => { iconImg.sprite = sprite; });

            valueTxt.text = $"x{resourceValue.value}";
        }

        public void SetValue(ResourceValue value) {
            resourceValue = value;
            SetupUI();
        }
    }
}