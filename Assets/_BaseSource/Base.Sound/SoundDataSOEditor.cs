// #if UNITY_EDITOR
// using System.Linq;
// using Sirenix.OdinInspector;
// using UnityEditor;
// using UnityEngine;
//
// namespace Base.Sound
// {
//     public partial class SoundDataSO
//     {
//         [Title("Editor Tools")]
//         private const string SOUND_CONST_SCRIPT = "SoundConst";
//         private const string SOUND_NAMESPACE = "Base.Sound";
//         private const string SOUND_GROUP = "SoundGroup";
//         private const string SOUND_NAME = "Sound";
//
//
//         [PropertySpace]
//         [HorizontalGroup("0")]
//         [Button(ButtonSizes.Large)]
//         [GUIColor(0.8f, 0.4f, 0f)]
//         public void GenerateSoundConstScript()
//         {
//             // find the directory where the script was generated
//             var g = AssetDatabase.FindAssets($"t:Script {nameof(SoundManager)}");
//             var scriptPath = AssetDatabase.GUIDToAssetPath(g[0]);
//             var folderPath = System.IO.Path.GetDirectoryName(scriptPath);
//             Debug.Log("Script will be generated in: ".Color("cyan") + folderPath.Color("yellow"));
//
//             // start building the class
//             var classBuilder = new System.Text.StringBuilder();
//             classBuilder.AppendLine($"namespace {SOUND_NAMESPACE}");
//             classBuilder.AppendLine("{");
//
//             #region === GENERATE SOUND GROUP ===
//             classBuilder.AppendLine($"\tpublic partial class {SOUND_GROUP}");
//             classBuilder.AppendLine("\t{");
//
//             foreach (var soundGroup in soundGroups)
//             {
//                 var groupName = soundGroup.groupName;
//                 var groupNameConst = GameUtils.ConvertToConst(groupName);
//                 classBuilder.AppendLine($"\t\tpublic const string {groupNameConst} = \"{groupName}\";");
//             }
//
//             classBuilder.AppendLine("\t}\n");
//             #endregion
//
//             #region === GENERATE SOUND NAME ===
//             classBuilder.AppendLine($"\tpublic partial class {SOUND_NAME}");
//             classBuilder.AppendLine("\t{");
//
//             foreach (var soundGroup in soundGroups)
//             {
//                 classBuilder.AppendLine($"\t\t// ===== GROUP: {soundGroup.groupName} =====");
//
//                 var soundNames = soundGroup.listSound.Select(s => s.clip.name).ToList();
//                 foreach (var soundName in soundNames)
//                 {
//                     var fieldName = GameUtils.ConvertToConst(soundName);
//                     classBuilder.AppendLine($"\t\tpublic const string {fieldName} = \"{soundName}\";");
//                 }
//
//                 if (soundGroup != soundGroups.Last())
//                     classBuilder.AppendLine();
//             }
//
//             classBuilder.AppendLine("\t}");
//             #endregion
//
//             classBuilder.AppendLine("}");
//
//             // write to the file
//             var scriptContent = classBuilder.ToString();
//             var path = $"{folderPath}/{SOUND_CONST_SCRIPT}.cs";
//             System.IO.File.WriteAllText(path, scriptContent);
//             Debug.Log($"Generated class {SOUND_CONST_SCRIPT}".Color("lime") + "\n" + scriptContent);
//         }
//
//
//         [PropertySpace]
//         [HorizontalGroup("0")]
//         [Button(ButtonSizes.Large)]
//         [GUIColor(0f, 0.8f, 0.4f)]
//         public static void OpenSoundConstScript()
//         {
//             // find the directory where the script was generated
//             var g = AssetDatabase.FindAssets($"t:Script {SOUND_CONST_SCRIPT}");
//             var scriptPath = AssetDatabase.GUIDToAssetPath(g[0]);
//
//             // Tải tệp script dựa trên đường dẫn
//             var scriptObject = AssetDatabase.LoadAssetAtPath(scriptPath, typeof(Object));
//
//             if (scriptObject != null)
//                 Selection.activeObject = scriptObject;
//             else
//                 Debug.LogError("Cannot find script file at path: " + scriptPath);
//         }
//     }
// }
// #endif