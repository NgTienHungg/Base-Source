using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Base.Editor
{
    public static class ImportPackageTool
    {
        // Đường dẫn tới file requirements.txt
        private const string RequirementsFilePath = "Assets/requirements.txt";

        [MenuItem("Tools/Packages/Import Requirements")]
        public static void ImportPackages() {
            var requirements = ReadRequirements(RequirementsFilePath).ToList();
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
                WriteRequirements(RequirementsFilePath, directory, requirements);
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
}