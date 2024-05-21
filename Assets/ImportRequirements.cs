#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

public static class ImportRequirements
{
    // Đường dẫn tới file requirements.txt
    private const string REQUIREMENTS_FILE_PATH = "Assets/requirements.txt";

    [MenuItem("Tools/Import Requirements")]
    public static void ImportPackages() {
        var requirements = ReadRequirements(REQUIREMENTS_FILE_PATH).ToList();
        var directory = requirements.First();
        requirements.RemoveAt(0);

        foreach (var packageName in requirements) {
            var packagePath = Path.Combine(directory, $"{packageName}.unitypackage");
            if (File.Exists(packagePath)) {
                AssetDatabase.ImportPackage(packagePath, false);
                Debug.Log($"Successfully imported {packageName}.");
            }
            else {
                Debug.LogError($"Not found package {packageName} at {packagePath}");
            }
        }
    }

    private static string[] ReadRequirements(string filePath) {
        if (!File.Exists(filePath)) {
            Debug.LogError($"Requirements file not found at {filePath}");
            return null;
        }

        try {
            return File.ReadAllLines(filePath);
        }
        catch (System.Exception ex) {
            Debug.LogError($"Failed to read requirements file: {ex.Message}");
            return null;
        }
    }
}
#endif