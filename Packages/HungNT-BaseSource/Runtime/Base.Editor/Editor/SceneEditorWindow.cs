using System.IO;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class SceneEditorWindow : OdinEditorWindow
    {
        [MenuItem("Base Source/Scene/Scene Editor Window")]
        private static void OpenWindow()
        {
            var window = GetWindow<SceneEditorWindow>();
            window.titleContent = new GUIContent("Scene Editor");
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var sceneName = Path.GetFileNameWithoutExtension(scenePath);

                if (SirenixEditorGUI.MenuButton(i, sceneName, true, null))
                {
                    OnChangeScene(scenePath);
                }
            }
        }

        private void OnChangeScene(string scenePath)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }

        [MenuItem("Base Source/Scene/Save & Play Game")]
        public static void QuickSaveAndPlay()
        {
            EditorSceneManager.SaveOpenScenes(); // Lưu scene hiện tại mà không hỏi
            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
            EditorApplication.isPlaying = true;
        }
    }
}