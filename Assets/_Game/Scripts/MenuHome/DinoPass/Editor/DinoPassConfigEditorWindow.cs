using Base.Data;
using UnityEditor;
using UnityEngine;

namespace Game.DinoPass
{
    public class DinoPassConfigEditorWindow : DataConfigEditorWindow<DinoPassDataConfig>
    {
        protected override string DataPath => Application.dataPath + GameConfig.FilePath.DinoPassDataConfig;

        [MenuItem("Game/Data Config/Dino Pass")]
        private static void OpenWindow()
        {
            GetWindow<DinoPassConfigEditorWindow>("Dino Pass Config").Show();
        }

        private void OnValidate()
        {
            DataConfig.Stages.ForEach(stage => stage.Id = DataConfig.Stages.IndexOf(stage));
        }
    }
}