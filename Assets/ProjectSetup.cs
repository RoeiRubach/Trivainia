using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using EditorUtility = UnityEditor.EditorUtility;

namespace rubach
{
    public static class ProjectSetup
    {
        [MenuItem("Tools/Setup/Import Essential Assets")]
        private static void ImportEssentials()
        {
            Assets.ImportAsset("Odin Inspector and Serializer.unitypackage", "Sirenix/Editor ExtensionsSystem");
            Assets.ImportAsset("Odin Validator.unitypackage", "Sirenix/Editor ExtensionsUtilities");
            Assets.ImportAsset("Selection History.unitypackage", "Staggart Creations/Editor ExtensionsUtilities");
            Assets.ImportAsset("Editor Auto Save.unitypackage", "IntenseNation/Editor ExtensionsUtilities");
            Assets.ImportAsset("PrimeTween High-Performance Animations and Sequences.unitypackage", "Kyrylo Kuzyk/Editor ExtensionsAnimation");
            Assets.ImportAsset("Better Hierarchy.unitypackage", "Toaster Head/Editor ExtensionsUtilities");
            
            Debug.Log("My essential assets have been imported.");
        }

        [MenuItem("Tools/Setup/Install Essential Packages")]
        public static void InstallPackages()
        {
            Packages.InstallPackages(new[] {
                "com.unity.addressables",
                "com.unity.cinemachine",
                "com.unity.ide.rider",
                "com.unity.splines",
                "com.unity.textmeshpro",
                "com.unity.shadergraph",
                "git+https://github.com/adammyhre/Unity-Utils.git",
                "git+https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
                "com.unity.inputsystem" // new Input System requires restart, import last
            });
        }
        
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            CreateDirectory(Combine(dataPath, "Documentations"));
            
            Folders.CreateDirectories("_Project", "Art", "Audio", "Code", "LevelDesign", "Scenes", "Documentation");
            Folders.CreateDirectories("_Project/Audio", "Music", "SFX");
            Folders.CreateDirectories("_Project/Art", "Materials", "Sprites", "Textures");
            Folders.CreateDirectories("_Project/Code", "Scripts", "Shaders");
            Folders.CreateDirectories("_Project/Code/Scripts", "Editor", "Runtime", "Tests");
            Folders.CreateDirectories("_Project/Code/Scripts/Tests", "Editor", "Runtime");
            Folders.CreateDirectories("_Project/LevelDesign", "Environment", "Units", "URPSettings");
            Refresh();
            Debug.Log("Default folders have been created.");
        }
        
        [MenuItem("Tools/Setup/Initialize Editor Settings")]
        public static void ImportEditorSettings()
        {
            Editors.InitializePlayerSettings();
            Editors.InitializeEditorSettings();
            Editors.InitializeMainScene();
            Editors.SetMyEditorLayout();
            Debug.Log("Editor settings have been initialized.");
        }

        private static class Assets
        {
            public static void ImportAsset(string asset, string folder)
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var assetsFolder = Combine(basePath, "Unity/Asset Store-5.x");
                ImportPackage(Combine(assetsFolder, folder, asset), false); // Change to 'true' to display the import dialog
            }
        }

        private static class Packages
        {
            private static AddRequest _request;
            private static Queue<string> _packagesToInstall = new();

            public static void InstallPackages(string[] packages)
            {
                foreach (var package in packages)
                    _packagesToInstall.Enqueue(package);

                if (_packagesToInstall.Count > 0)
                    StartNextPackageInstallation();
            }

            private static async void StartNextPackageInstallation()
            {
                _request = Client.Add(_packagesToInstall.Dequeue());

                while (!_request.IsCompleted) await Task.Delay(10);
                
                if (_request.Status == StatusCode.Success) Debug.Log("Installed: " + _request.Result.packageId);
                else if (_request.Status >= StatusCode.Failure) Debug.Log(_request.Error.message);

                if (_packagesToInstall.Count > 0)
                {
                    await Task.Delay(1000);
                    StartNextPackageInstallation();
                }
            }
        }

        private static class Folders
        {
            public static void CreateDirectories(string root, params string[] directories)
            {
                var fullPath = Combine(dataPath, root);

                foreach (var newDirectory in directories)
                    CreateDirectory(Combine(fullPath, newDirectory));
            }

            public static void Delete(string folderName)
            {
                var pathToDelete = $"Assets/{folderName}";

                if (IsValidFolder(pathToDelete))
                    DeleteAsset(pathToDelete);
            }
        }

        private static class Editors
        {
            public static void InitializePlayerSettings()
            {
                PlayerSettings.companyName = "DivainStudio";
                PlayerSettings.colorSpace = ColorSpace.Linear;
            }
            
            public static void InitializeEditorSettings() => EditorSettings.projectGenerationRootNamespace = PlayerSettings.productName;
            
            public static void SetMyEditorLayout()
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var assetsFolder = Combine(basePath, "Unity/Editor-5.x");
                const string LAYOUT_PATH = "Preferences/Layouts/default/My Layout.wlt";
                EditorUtility.LoadWindowLayout(Combine(assetsFolder, LAYOUT_PATH));
            }

            public static void InitializeMainScene()
            {
                const string SCENE_PATH = "Assets/_Project/Scenes/Main.unity";
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), SCENE_PATH);
                var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
                if (!string.IsNullOrEmpty(SCENE_PATH))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(SCENE_PATH, true));
                EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            }
        }
    }
}