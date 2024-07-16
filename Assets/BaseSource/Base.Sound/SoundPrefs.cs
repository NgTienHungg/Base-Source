// using UnityEngine;
//
// namespace Base.Sound
// {
//     public static class SoundPrefs
//     {
//         private const string MUSIC_ON = "OnMusic";
//         private const string SOUND_ON = "OnSound";
//         private const string VIBRATION_ON = "OnVibration";
//
//         public static bool IsFirstTimeInGame => !PlayerPrefs.HasKey(MUSIC_ON);
//
//         public static void SetDefaultSettings()
//         {
//             MusicOn = true;
//             SoundOn = true;
//             VibrationOn = true;
//             GamePrefs.OnQuickBall = true;
//         }
//
//         public static bool MusicOn
//         {
//             get => PlayerPrefs.GetInt(MUSIC_ON) == 1;
//             set => PlayerPrefs.SetInt(MUSIC_ON, value ? 1 : 0);
//         }
//
//         public static bool SoundOn
//         {
//             get => PlayerPrefs.GetInt(SOUND_ON) == 1;
//             set => PlayerPrefs.SetInt(SOUND_ON, value ? 1 : 0);
//         }
//
//         public static bool VibrationOn
//         {
//             get => PlayerPrefs.GetInt(VIBRATION_ON) == 1;
//             set => PlayerPrefs.SetInt(VIBRATION_ON, value ? 1 : 0);
//         }
//     }
// }