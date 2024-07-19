using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.UI
{
    public class UIButtonPlaySound : UIButtonBase
    {
        [SerializeField] [ValueDropdown("@GameConfig.SoundName.GetAllStrings")]
        private string sfxAddress;

        [SerializeField] [Range(0, 1)]
        private float volume = 1;

        protected override void OnClick()
        {
            // var audio = await ResourcesLoader.Instance.LoadAsync<AudioClip>(sfxAddress);
            // Sound.Controller.Instance.PlayOneShot(audio, volume);
        }
    }
}