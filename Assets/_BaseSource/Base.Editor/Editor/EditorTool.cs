using System.Diagnostics;
using BayatGames.SaveGameFree;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Base.Editor
{
    public static class EditorTool
    {
        [MenuItem("Tools/HungNT/Clear Save Data")]
        private static void ClearData() {
            PlayerPrefs.DeleteAll(); // Xóa PlayerPref
            SaveGame.Clear();
            Debug.Log("Data cleared!".Color("lime"));
        }

        [MenuItem("Tools/HungNT/Open Save Folder")]
        public static void OpenSaveFolder() {
            string saveFolderPath = Application.persistentDataPath;

            if (!string.IsNullOrEmpty(saveFolderPath)) {
                Process.Start("explorer.exe", saveFolderPath.Replace("/", "\\"));
            }
            else {
                Debug.LogError("Save data folder path is invalid or could not be found.");
            }
        }

        [MenuItem("Tools/HungNT/Play Immediately")]
        public static void SwitchToSceneLoadingAndPlay() {
            EditorSceneManager.SaveOpenScenes(); // Lưu scene hiện tại
            EditorSceneManager.SaveOpenScenes(); // Lưu scene hiện tại
            var scenePath = SceneUtility.GetScenePathByBuildIndex(0);
            EditorSceneManager.OpenScene(scenePath);
            EditorApplication.isPlaying = true; // Bật chế độ chơi game
        }

        [MenuItem("Tools/HungNT/Switch Next Scene")]
        public static void SwitchNextScene() {
            var currentScene = SceneManager.GetActiveScene();
            var nextSceneIndex = currentScene.buildIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                nextSceneIndex = 0;
            var nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            EditorSceneManager.OpenScene(nextScenePath);
            Debug.Log($"Switch to scene {nextSceneIndex}!".Color("lime"));
        }
    }
}