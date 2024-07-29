// using System;
// using System.Collections.Generic;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace Base.Sound
// {
//     [CreateAssetMenu(menuName = "GameData/SoundDataSO", fileName = nameof(SoundDataSO))]
//     public partial class SoundDataSO : ScriptableObject
//     {
//         [Title("Sound Prefs")]
//         [ShowInInspector, PropertyOrder(-1)]
//         private bool MusicOn => SoundPrefs.MusicOn;
//
//         [ShowInInspector, PropertyOrder(-1)]
//         private bool SoundOn => SoundPrefs.SoundOn;
//
//         [ShowInInspector, PropertyOrder(-1)]
//         private bool VibrationOn => SoundPrefs.VibrationOn;
//
//         [Title("List Sounds")]
//         public List<SoundGroup> soundGroups = new List<SoundGroup>();
//     }
//
//
//     [Serializable]
//     public partial class SoundGroup
//     {
//         [HorizontalGroup("0", Width = 0.30f)]
//         [HideLabel, GUIColor("#ffff80")]
//         public string groupName;
//
//         [HideLabel]
//         [HorizontalGroup("0")]
//         public bool isMusic;
//
//         [HorizontalGroup("1")]
//         [LabelText("$groupName")]
//         public List<Sound> listSound = new List<Sound>();
//     }
//
//
//     [Serializable]
//     public partial class Sound
//     {
//         [InlineButton(nameof(Play), SdfIconType.Play)]
//         public AudioClip clip;
//
//         [Range(0f, 1f)]
//         public float volume = 1f;
//
//         [Range(-3f, 3f)]
//         public float pitch = 1f;
//
//         private void Play()
//         {
//             SoundManager.Instance.PlayTestSound(clip, volume, pitch);
//         }
//     }
// }