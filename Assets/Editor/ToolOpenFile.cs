using System.IO;
using UnityEditor;
using UnityEngine;

public static class ToolOpenFile
{
    [MenuItem("Tools/Packages/Open manifest.json")]
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

    [MenuItem("Tools/Packages/Open requirements.txt")]
    public static void OpenRequirementsTxtFile() {
        string projectPath = Application.dataPath;
        string requirementsPath = Path.Combine(projectPath, "requirements.txt");

        if (File.Exists(requirementsPath)) {
            OpenFileInDefaultEditor(requirementsPath);
        }
        else {
            Debug.LogError($"requirements.txt file not found at {requirementsPath}");
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