using System.IO;
using UnityEditor;
using UnityEngine;

public static class OpenManifestJson
{
    private const string MENU_ITEM_NAME = "Tools/Open manifest.json";

    [MenuItem(MENU_ITEM_NAME)]
    public static void OpenManifestJsonFile() {
        string projectPath = Application.dataPath;
        string manifestPath = Path.Combine(projectPath, "../Packages/manifest.json");
        manifestPath = Path.GetFullPath(manifestPath);

        if (File.Exists(manifestPath)) {
            OpenFileInDefaultEditor(manifestPath);
        }
        else {
            Debug.LogError($"manifest.json file not found at {manifestPath}");
        }
    }

    private static void OpenFileInDefaultEditor(string filePath) {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = new System.Diagnostics.ProcessStartInfo() {
            FileName = filePath,
            UseShellExecute = true,
            Verb = "open"
        };
        process.Start();
    }
}