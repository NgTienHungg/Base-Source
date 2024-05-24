// #if UNITY_EDITOR
// using System.Linq;
// using UnityEditor;
// using UnityEngine;
// using Sirenix.OdinInspector;
//
// namespace Base.Pool
// {
//     public partial class PoolDataSO
//     {
//         private const string POOL_CONST_SCRIPT = "PoolConst";
//         private const string POOL_NAMESPACE = "Base.Pool";
//         private const string POOL_NAME = "Pool";
//
//
//         [PropertySpace]
//         [HorizontalGroup("0")]
//         [Button(ButtonSizes.Large)]
//         [GUIColor(0.8f, 0.4f, 0f)]
//         private void GeneratePoolNameScript()
//         {
//             // get all prefab names
//             var prefabNames = prefabs.Select(go => go.name).ToList();
//
//             // find the directory where the script was generated
//             var g = AssetDatabase.FindAssets($"t:Script {nameof(PoolManager)}");
//             var scriptPath = AssetDatabase.GUIDToAssetPath(g[0]);
//             var folderPath = System.IO.Path.GetDirectoryName(scriptPath);
//             Debug.Log("Script will be generated in: ".Color("cyan") + folderPath.Color("yellow"));
//
//             // start building the class
//             var classBuilder = new System.Text.StringBuilder();
//             classBuilder.AppendLine($"namespace {POOL_NAMESPACE}");
//             classBuilder.AppendLine("{");
//             classBuilder.AppendLine($"\tpublic static class {POOL_NAME}");
//             classBuilder.AppendLine("\t{");
//
//             foreach (var prefabName in prefabNames)a
//             {
//                 var fieldName = GameUtils.ConvertToConst(prefabName);
//                 classBuilder.AppendLine($"\t\tpublic const string {fieldName} = \"{prefabName}\";");
//             }
//
//             classBuilder.AppendLine("\t}");
//             classBuilder.AppendLine("}");
//
//             // write to the file
//             var scriptContent = classBuilder.ToString();
//             var path = $"{folderPath}/{POOL_CONST_SCRIPT}.cs";
//             System.IO.File.WriteAllText(path, scriptContent);
//             Debug.Log($"Generated class {POOL_NAME}".Color("lime") + "\n" + scriptContent);
//         }
//
//
//         [PropertySpace]
//         [HorizontalGroup("0")]
//         [Button(ButtonSizes.Large)]
//         [GUIColor(0f, 0.8f, 0.4f)]
//         private void OpenPoolNameScript()
//         {
//             // find the directory where the script was generated
//             var g = AssetDatabase.FindAssets($"t:Script {POOL_NAME}");
//             var scriptPath = AssetDatabase.GUIDToAssetPath(g[0]);
//
//             // Load script file based on path
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