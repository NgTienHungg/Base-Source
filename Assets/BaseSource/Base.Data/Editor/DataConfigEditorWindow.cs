using System;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Base.Data
{
    public class DataConfigEditorWindow<T> : OdinEditorWindow
    {
        [HideLabel]
        public T DataConfig;

        protected virtual string DataPath { get; }

        protected override void OnEnable()
        {
            base.OnEnable();
            LoadData();
        }

        [PropertySpace]
        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor("yellow")]
        private async void LoadData()
        {
            var content = await File.ReadAllTextAsync(DataPath);
            DataConfig = JsonConvert.DeserializeObject<T>(content);
            Debug.Log("[Editor] Load Data Success!".Color("cyan"));
        }

        [PropertySpace]
        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor("green")]
        private void SaveData()
        {
            var content = JsonConvert.SerializeObject(DataConfig);
            File.WriteAllText(DataPath, content);
            Debug.Log("[Editor] Save Data Success!".Color("lime"));
        }

        [PropertySpace]
        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor("cyan")]
        private void SelectFile()
        {
            var assetPath = DataPath.Substring(DataPath.IndexOf("Assets", StringComparison.Ordinal));
            var file = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            Selection.activeObject = file;
        }
    }
}