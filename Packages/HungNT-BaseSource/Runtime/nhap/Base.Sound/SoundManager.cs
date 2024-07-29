// using System;
// using UnityEngine;
// using System.Collections.Generic;
// using MoreMountains.NiceVibrations;
// using Base.Singleton;
// using Cysharp.Threading.Tasks;
// using Sirenix.OdinInspector;
//
// namespace Base.Sound
// {
//     [RequireComponent(typeof(AudioSource))]
//     public class SoundManager : SingletonPersistent<SoundManager>
//     {
//         private static readonly List<Sound> allSounds = new List<Sound>();
//         private static readonly List<Sound> allMusics = new List<Sound>();
//         private static readonly List<AudioSource> audioSourcePool = new List<AudioSource>();
//
//         private SoundDataSO _soundData;
//
//
//         protected override void OnAwake()
//         {
//             // load data from Resources
//             _soundData = Resources.Load<SoundDataSO>(nameof(SoundDataSO));
//
//             // add sounds & musics to list
//             foreach (var soundGroup in _soundData.soundGroups)
//             {
//                 if (soundGroup.isMusic)
//                     allMusics.AddRange(soundGroup.listSound);
//                 else
//                     allSounds.AddRange(soundGroup.listSound);
//             }
//
//             // add an AudioSource if use PlayOneShot
//             audioSourcePool.Add(Instance.GetComponent<AudioSource>());
//
//             // check if first time in game, set ON for all settings
//             if (SoundPrefs.IsFirstTimeInGame)
//             {
//                 Debug.Log("[SoundManager] first time in game, set ON for all settings");
//                 SoundPrefs.SetDefaultSettings();
//             }
//         }
//
//
//         private static AudioSource GetAudioSourceForSound(Sound sound)
//         {
//             // find free audioSource in pool
//             var audioSource = audioSourcePool.Find(source => !source.isPlaying);
//
//             // add new AudioSource component into pool
//             if (audioSource == null)
//             {
//                 audioSource = Instance.gameObject.AddComponent<AudioSource>();
//                 audioSourcePool.Add(audioSource);
//             }
//
//             // set value of sound for audioSource
//             audioSource.loop = false;
//             audioSource.clip = sound.clip;
//             audioSource.pitch = sound.pitch;
//             audioSource.volume = sound.volume;
//
//             return audioSource;
//         }
//
//
//         private static Sound FindSoundByName(string soundName)
//         {
//             try {
//                 var sound = allSounds.Find(sound => sound.clip.name == soundName);
//                 
//                 if (sound == null)
//                 {
//                     Debug.LogWarning("Can't find sound with name " + soundName.Color("red"));
//                     return null;
//                 }
//     
//                 return sound;
//             }
//             catch (Exception e) {
//                 Debug.LogWarning("Can't find sound with name " + soundName.Color("red"));
//                 return null;
//             }
//         }
//
//
//         private static Sound FindMusicByName(string musicName)
//         {
//             var music = allMusics.Find(music => music.clip.name == musicName);
//
//             if (music == null)
//             {
//                 Debug.LogWarning("Can't find music with name " + musicName.Color("red"));
//                 return null;
//             }
//
//             return music;
//         }
//
//
//         public static void PlaySound(string soundName)
//         {
//             if (!SoundPrefs.SoundOn) return;
//
//             var sound = FindSoundByName(soundName);
//
//             if (sound != null)
//             {
//                 var audioSource = GetAudioSourceForSound(sound);
//                 audioSource.Play();
//             }
//         }
//
//
//         public static void PlaySoundOneShot(string soundName)
//         {
//             if (!SoundPrefs.SoundOn) return;
//
//             var sound = FindSoundByName(soundName);
//
//             if (sound != null)
//             {
//                 // can use only one audioSource to play one shot
//                 var audioSource = audioSourcePool[0];
//                 audioSource.PlayOneShot(sound.clip);
//             }
//         }
//
//
//         public static void PlaySoundInGroup(string groupName, int index = -1)
//         {
//             var soundGroup = Instance._soundData.soundGroups.Find(x => x.groupName == groupName);
//
//             if (soundGroup == null)
//             {
//                 Debug.LogWarning("Can't find sound group with name " + groupName.Color("red"));
//                 return;
//             }
//
//             switch (index)
//             {
//                 case -1:
//                     PlaySound(soundGroup.listSound.Rand().clip.name);
//                     break;
//                 case >= 0 when index < soundGroup.listSound.Count:
//                     PlaySound(soundGroup.listSound[index].clip.name);
//                     break;
//                 default:
//                     Debug.LogWarning($"Index {index.ToString().Color("red")} out range of group {groupName.Color("red")}");
//                     break;
//             }
//         }
//
//
//         public static void PlayOneShotSoundInGroup(string groupName, int index = -1)
//         {
//             var soundGroup = Instance._soundData.soundGroups.Find(x => x.groupName == groupName);
//
//             if (soundGroup == null)
//             {
//                 Debug.LogWarning("Can't find sound group with name " + groupName.Color("red"));
//                 return;
//             }
//
//             switch (index)
//             {
//                 case -1:
//                     PlaySoundOneShot(soundGroup.listSound.Rand().clip.name);
//                     break;
//                 case >= 0 when index < soundGroup.listSound.Count:
//                     PlaySoundOneShot(soundGroup.listSound[index].clip.name);
//                     break;
//                 default:
//                     Debug.LogWarning($"Index {index.ToString().Color("red")} out range of group {groupName.Color("red")}");
//                     break;
//             }
//         }
//
//
//         public static async void PlaySoundInTime(string soundName, float duration)
//         {
//             if (!SoundPrefs.SoundOn) return;
//
//             var sound = FindSoundByName(soundName);
//
//             if (sound != null)
//             {
//                 var audioSource = GetAudioSourceForSound(sound);
//                 audioSource.Play();
//
//                 // wait to stop sound
//                 await UniTask.Delay(TimeSpan.FromSeconds(duration));
//                 audioSource.Stop();
//             }
//         }
//
//
//         public static void PlayMusic(string musicName, bool loop = false)
//         {
//             if (!SoundPrefs.MusicOn) return;
//
//             var music = FindMusicByName(musicName);
//
//             if (music != null)
//             {
//                 var audioSource = GetAudioSourceForSound(music);
//                 audioSource.loop = loop;
//                 audioSource.Play();
//             }
//         }
//
//
//         public static void PlayRandomMusicInGroup(string groupName, int index = -1, bool loop = false)
//         {
//             var musicGroup = Instance._soundData.soundGroups.Find(x => x.groupName == groupName);
//
//             if (musicGroup == null)
//             {
//                 Debug.LogWarning("Can't find music group with name " + groupName.Color("red"));
//                 return;
//             }
//
//             switch (index)
//             {
//                 case -1:
//                     PlayMusic(musicGroup.listSound.Rand().clip.name, loop);
//                     break;
//                 case >= 0 when index < musicGroup.listSound.Count:
//                     PlayMusic(musicGroup.listSound[index].clip.name, loop);
//                     break;
//                 default:
//                     Debug.LogWarning($"Index {index.ToString().Color("red")} out range of group {groupName.Color("red")}");
//                     break;
//             }
//         }
//
//
//         public static void PlayMusicInTime(string musicName, float duration)
//         {
//             if (!SoundPrefs.MusicOn) return;
//
//             var music = FindMusicByName(musicName);
//
//             if (music != null)
//             {
//                 var audioSource = GetAudioSourceForSound(music);
//                 audioSource.loop = true;
//                 audioSource.Play();
//
//                 // wait to stop music
//                 GameUtils.DelayAction(duration.Millisecond(), () => audioSource.Stop());
//             }
//         }
//
//
//         public static void StopMusic(string musicName)
//         {
//             var audioSourcePlayingMusic = audioSourcePool.Find(
//                 source => source.isPlaying && source.clip.name == musicName);
//
//             audioSourcePlayingMusic?.Stop();
//         }
//
//
//         public static void StopAllMusicInGroup(string groupName)
//         {
//             var soundGroup = Instance._soundData.soundGroups.Find(x => x.groupName == groupName);
//             soundGroup.listSound.ForEach(sound => StopMusic(sound.clip.name));
//         }
//
//
//         public static void StopAllMusic()
//         {
//             allMusics.ForEach(music => StopMusic(music.clip.name));
//         }
//
//
//         public static void Vibrate(HapticTypes type)
//         {
//             if (SoundPrefs.VibrationOn)
//                 MMVibrationManager.Haptic(type);
//         }
//
//         public void PlayTestSound(AudioClip clip, float volume, float pitch)
//         {
//             var audioSource = GetComponent<AudioSource>();
//             audioSource.clip = clip;
//             audioSource.volume = volume;
//             audioSource.pitch = pitch;
//             audioSource.Play();
//         }
//
// #if UNITY_EDITOR
//         [Button(ButtonSizes.Medium)]
//         private void OpenSoundDataSO()
//         {
//             UnityEditor.Selection.activeObject = Resources.Load<SoundDataSO>(nameof(SoundDataSO));
//         }
// #endif
//     }
// }