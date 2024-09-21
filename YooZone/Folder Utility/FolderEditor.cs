using System.IO;
using UnityEditor;
using UnityEngine;

namespace YooZone.FolderUtility {
    public class FolderEditor : Editor {
        [MenuItem("YooZone/Create Folder")]
        public static void Execute() {
            var assets = GenerateFolderStructure();
            CreateFolders(assets);

            if (AssetDatabase.IsValidFolder($"Assets/{Application.productName}")) {
                Move(Application.productName, "Scenes");
                Delete("TutorialInfo");
                AssetDatabase.Refresh();

                if (AssetDatabase.IsValidFolder($"Assets/{Application.productName}/Settings")) {
                    if (AssetDatabase.IsValidFolder($"Assets/{Application.productName}/Settings/Renderer")) {
                        AssetDatabase.MoveAsset("Assets/GlobalVolumeProfile.asset", $"Assets/{Application.productName}/Settings/Renderer/GlobalVolumeProfile.asset");
                        AssetDatabase.MoveAsset("Assets/UniversalRenderPipelineGlobalSettings.asset", $"Assets/{Application.productName}/Settings/Renderer/UniversalRenderPipelineGlobalSettings.asset");
                        AssetDatabase.MoveAsset($"Assets/Settings/Renderer2D.asset", $"Assets/{Application.productName}/Settings/Renderer/Renderer2D.asset");
                        AssetDatabase.MoveAsset($"Assets/Settings/UniversalRP.asset", $"Assets/{Application.productName}/Settings/Renderer/UniversalRP.asset");
                        AssetDatabase.MoveAsset($"Assets/Settings/Scenes/URP2DSceneTemplate.unity", $"Assets/{Application.productName}/Settings/Renderer/Scenes/URP2DSceneTemplate.unity");
                        AssetDatabase.MoveAsset($"Assets/Settings/Lit2DSceneTemplate.scenetemplate", $"Assets/{Application.productName}/Settings/Renderer/Lit2DSceneTemplate.scenetemplate");

                        AssetDatabase.DeleteAsset("Assets/Settings");
                        AssetDatabase.Refresh();
                    }

                    if (AssetDatabase.IsValidFolder($"Assets/{Application.productName}/Settings/Resources")) {
                        AssetDatabase.MoveAsset("Assets/InputSystem_Actions.inputactions", $"Assets/{Application.productName}/Settings/Resources/InputSystem_Actions.inputactions");
                        AssetDatabase.Refresh();
                    }

                    AssetDatabase.Refresh();
                }

                AssetDatabase.MoveAsset("Assets/YooZone.dll", $"Assets/{Application.productName}/Plugins/Editor/YooZone.dll");

                AssetDatabase.DeleteAsset("Assets/Readme.asset");

                AssetDatabase.Refresh();
            }
        }

        [MenuItem("YooZone/Create Folder", true, 0)]
        public static bool ValidateExecute() {
            return !AssetDatabase.IsValidFolder($"Assets/{Application.productName}");
        }

        public static void Move(string newParentFolder, string folderName) {
            var sourcePath = $"Assets/{folderName}";

            if (AssetDatabase.IsValidFolder(sourcePath)) {
                var destinationFolder = $"Assets/{newParentFolder}/{folderName}";

                var error = AssetDatabase.MoveAsset(sourcePath, destinationFolder);

                if (!string.IsNullOrEmpty(error)) {
                    Debug.LogError($"Failed to move {folderName}: {error}");
                }
            }
        }

        public static void Delete(string folderName) {
            var pathToDelete = $"Assets/{folderName}";

            if (AssetDatabase.IsValidFolder(pathToDelete)) {
                AssetDatabase.DeleteAsset(pathToDelete);
            }
        }

        private static void CreateFolders(Folder rootFolder) {
            if (!AssetDatabase.IsValidFolder(rootFolder.CurrentFolder)) {
                Debug.Log("Creating: <b>" + rootFolder.CurrentFolder + "</b>");

                AssetDatabase.CreateFolder(rootFolder.ParentFolder, rootFolder.Name);

                File.Create(Directory.GetCurrentDirectory()
                                + Path.DirectorySeparatorChar
                                + rootFolder.CurrentFolder
                                + Path.DirectorySeparatorChar
                                + ".keep");
            } else {
                if (Directory.GetFiles(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + rootFolder.CurrentFolder).Length < 1) {

                    File.Create(Directory.GetCurrentDirectory()
                                + Path.DirectorySeparatorChar
                                + rootFolder.CurrentFolder
                                + Path.DirectorySeparatorChar
                                + ".keep");

                    Debug.Log("Creating '.keep' file in: <b>" + rootFolder.CurrentFolder + "</b>");
                } else {
                    Debug.Log("Directory <b>" + rootFolder.CurrentFolder + "</b> already exists");
                }
            }

            foreach (var folder in rootFolder.Folders) {
                CreateFolders(folder);
            }
        }

        private static Folder GenerateFolderStructure() {
            var rootFolder = new Folder("Assets", "");
            rootFolder.Add("ThirdParty");

            var subFolder = rootFolder.Add(Application.productName);
            subFolder.Add("Effects");

            var pluginFolder = subFolder.Add("Plugins");
            pluginFolder.Add("Editor");

            subFolder.Add("Prefabs");
            subFolder.Add("ScriptableObjects");

            var scriptFolder = subFolder.Add("Scripts");
            scriptFolder.Add("Editor");
            scriptFolder.Add("Runtime");

            var testFolder = subFolder.Add("Tests");
            testFolder.Add("Editor");
            testFolder.Add("Runtime");

            var artFolder = subFolder.Add("Art");
            artFolder.Add("Animations");
            artFolder.Add("Fonts");
            artFolder.Add("Materials");
            artFolder.Add("Meshes");
            artFolder.Add("Textures");
            artFolder.Add("Shaders");
            artFolder.Add("Sprites");
            artFolder.Add("Audio");

            var settingsFolder = subFolder.Add("Settings");
            var uiFolder = settingsFolder.Add("UIToolkit");
            uiFolder.Add("Layouts");
            uiFolder.Add("Settings");
            uiFolder.Add("Styles");
            uiFolder.Add("Theme");

            var rendererFolder = settingsFolder.Add("Renderer");
            rendererFolder.Add("Scenes");

            settingsFolder.Add("Presets");
            settingsFolder.Add("Resources");

            return rootFolder;
        }
    }
}
