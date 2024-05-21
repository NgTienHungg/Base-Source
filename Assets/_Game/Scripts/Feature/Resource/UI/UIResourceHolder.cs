﻿using Base.Asset;
using Base.Core;
using Base.Data;
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

        private void SetupUI() {
            AssetLoader.LoadSprite(Address.ResourceAtlas, resourceType.ToString())
                .ContinueWith(sprite => { iconImg.sprite = sprite; });

            valueTxt.text = DataManager.Datasave.Resource.GetResource(resourceType).ToString();
        }

        private void OnEnable() {
            SetupUI();

            DataManager.Datasave.Resource.OnResourceChanged += OnResourceChanged;
        }

        private void OnDisable() {
            DataManager.Datasave.Resource.OnResourceChanged -= OnResourceChanged;
        }

        private void OnResourceChanged(EResource type, int valueChanged) {
            if (type == resourceType) {
                valueTxt.text = DataManager.Datasave.Resource.GetResource(resourceType).ToString();
            }
        }
    }
}