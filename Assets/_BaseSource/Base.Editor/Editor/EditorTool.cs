using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Base.Editor
{
    public static class EditorTool
    {
        [MenuItem("Tools/Open/Open Save Folder")]
        public static void OpenSaveFolder() {
            string saveFolderPath = Application.persistentDataPath;

            if (!string.IsNullOrEmpty(saveFolderPath)) {
                Process.Start("explorer.exe", saveFolderPath.Replace("/", "\\"));
            }
            else {
                Debug.LogError("Save data folder path is invalid or could not be found.");
            }
        }
    }
}