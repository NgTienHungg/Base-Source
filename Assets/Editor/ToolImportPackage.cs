using System.IO;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

public static class ToolImportPackage
{
    // Đường dẫn tới file requirements.txt
    private const string REQUIREMENTS_FILE_PATH = "Assets/requirements.txt";

    [MenuItem("Tools/Packages/Import Requirements")]
    public static void ImportPackages() {
        var requirements = ReadRequirements(REQUIREMENTS_FILE_PATH).ToList();
        var directory = requirements.First();
        requirements.RemoveAt(0);

        bool changesMade = false;

        for (int i = 0; i < requirements.Count; i++) {
            var packageName = requirements[i];
            if (packageName.StartsWith("-")) {
                continue; // Skip already imported packages
            }

            var packagePath = Path.Combine(directory, $"{packageName}.unitypackage");
            if (File.Exists(packagePath)) {
                AssetDatabase.ImportPackage(packagePath, false);
                Debug.Log($"Successfully imported {packageName}.");

                // Mark package as imported
                requirements[i] = "-" + packageName;
                changesMade = true;
            }
            else {
                Debug.LogError($"Not found package {packageName} at {packagePath}");
            }
        }

        if (changesMade) {
            WriteRequirements(REQUIREMENTS_FILE_PATH, directory, requirements);
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

    private static void WriteRequirements(string filePath, string directory, System.Collections.Generic.List<string> requirements) {
        try {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                writer.WriteLine(directory);
                foreach (var requirement in requirements) {
                    writer.WriteLine(requirement);
                }
            }
        }
        catch (System.Exception ex) {
            Debug.LogError($"Failed to write requirements file: {ex.Message}");
        }
    }
}