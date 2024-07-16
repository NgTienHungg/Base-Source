using Base.Asset;
using Base.Data;
using Base.Core;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Resource
{
    public class UIResourceHolder : MonoBehaviour
    {
        [SerializeField]
        private EResource resourceType;

        [SerializeField]
        private Image iconImg;

        [SerializeField]
        private TextMeshProUGUI valueTxt;

        private void SetupUI()
        {
            AssetLoader.Instance.LoadSprite(GameConfig.Address.ResourceAtlas, resourceType.ToString())
                .ContinueWith(sprite => { iconImg.sprite = sprite; });

            valueTxt.text = DataManager.Datasave.Resource.GetResource(resourceType).ToString();
        }

        private void OnEnable()
        {
            SetupUI();

            DataManager.Datasave.Resource.OnResourceChanged += OnResourceChanged;
        }

        private void OnDisable()
        {
            DataManager.Datasave.Resource.OnResourceChanged -= OnResourceChanged;
        }

        private void OnResourceChanged(EResource type, int valueChanged)
        {
            if (type == resourceType)
            {
                valueTxt.text = DataManager.Datasave.Resource.GetResource(resourceType).ToString();
            }
        }
    }
}