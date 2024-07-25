using System;
using UnityEngine;

namespace Game.DinoPass
{
    public class UIActiveDinoPassButton : MonoBehaviour
    {
        // private void Awake()
        // {
        //     this.RegisterListener(EventInGameConfig.OnActivateDinoPass, HideButton);
        // }
        //
        // private void OnDestroy()
        // {
        //     this.RemoveListener(EventInGameConfig.OnActivateDinoPass, HideButton);
        // }

        private void OnEnable()
        {
            // gameObject.SetActive(!ProfileManager.Instance.UserData.DinoPassDataSave.IsActiveVip);
        }

        private void HideButton(object obj)
        {
            gameObject.SetActive(false);
        }
    }
}