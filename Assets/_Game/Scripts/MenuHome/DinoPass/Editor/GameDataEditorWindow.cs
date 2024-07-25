using System;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Object = UnityEngine.Object;

namespace WingsMob.BoatPacking
{
    public class GameDataEditorWindow<T> : OdinEditorWindow
    {
        protected virtual string DataPath { get; }

        public T DataConfig;

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
            // Common.Log("[Editor] Load Dino Pass Data Success!".Color("cyan"));
        }

        [PropertySpace]
        [HorizontalGroup("Actions")]
        [Button(ButtonSizes.Large), GUIColor("green")]
        private void SaveData()
        {
            var content = JsonConvert.SerializeObject(DataConfig);
            File.WriteAllText(DataPath, content);
            // Common.Log("[Editor] Save Dino Pass Data Success!".Color("lime"));
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