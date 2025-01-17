﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools.SmartNS.Editor{

    public class SmartNSBulkConversionWindow : EditorWindow {
        [MenuItem("Window/SmartNS/Bulk Namespace Conversion...")]
        static void Init() {
            SmartNSBulkConversionWindow window =
                (SmartNSBulkConversionWindow) EditorWindow.GetWindow(typeof(SmartNSBulkConversionWindow));
            window.titleContent = new GUIContent("Bulk Namespace Converter");
            window.Show();
        }

        private string _baseDirectory = "";
        private bool _isProcessing = false;
        private List<string> _assetsToProcess;
        private int _progressCount;


        private string _scriptRootSettingsValue;
        private string _prefixSettingsValue;
        private string _universalNamespaceSettingsValue;
        private bool _useSpacesSettingsValue;
        private int _numberOfSpacesSettingsValue;
        private string _directoryDenyListSettingsValue;
        private bool _enableDebugLogging;

        private HashSet<string> _ignoredDirectories;


        private static string GetClickedDirFullPath() {
            if (Selection.assetGUIDs.Length > 0) {
                var clickedAssetGuid = Selection.assetGUIDs[0];
                var clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);
                var clickedPathFull = Path.Combine(Directory.GetCurrentDirectory(), clickedPath);
                var attr = File.GetAttributes(clickedPathFull);

                if (attr.HasFlag(FileAttributes.Directory)) {
                    return clickedPath;
                }
                else {
                    var lastForwardSlashIndex = clickedPath.LastIndexOf('/');
                    var lastBackSlashIndex = clickedPath.LastIndexOf('\\');

                    if (lastForwardSlashIndex >= 0) {
                        return clickedPath.Substring(0, lastForwardSlashIndex);
                    }
                    else if (lastBackSlashIndex >= 0) {
                        return clickedPath.Substring(0, lastBackSlashIndex);
                    }
                }
            }
            return null;
        }

        private void ProcessRenameAllAsmdefFiles()
        {
            var smartNSSettings = SmartNSSettings.GetSerializedSettings();
            _scriptRootSettingsValue = smartNSSettings.FindProperty("_ScriptRoot").stringValue;

            var asmdefFiles = GetAssetsToProcessAsmdefFiles(_scriptRootSettingsValue);
            foreach (var asmdefFile in asmdefFiles) {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(asmdefFile);
                string newFileName = asmdefFile.Replace(_scriptRootSettingsValue, "");
                newFileName = newFileName.Replace(fileNameWithoutExtension, "");
                newFileName = newFileName.Replace("/", ".");
                newFileName = newFileName.Replace("..", ".");
                newFileName = newFileName.Remove(0, 1);
                 
                var asmdefObject = AssetDatabase.LoadMainAssetAtPath(asmdefFile);
                if (asmdefObject != null)
                {
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(asmdefObject), newFileName);
                
                    Debug.Log($"Renaming {asmdefFile} to {newFileName}.asmdef ");
                }
                else
                {
                    Debug.LogError($"Failed to load asset at path {asmdefFile}");
                }
            }
        }

        void OnGUI() {
            var yPos = 20;

            if (string.IsNullOrWhiteSpace(_baseDirectory)) {
                _baseDirectory = GetClickedDirFullPath();
            }

            if (string.IsNullOrWhiteSpace(_baseDirectory)) {
                _baseDirectory = "Assets";
            }

            GUILayout.Label("SmartNS Bulk Namespace Conversion", EditorStyles.boldLabel);
            GUI.Box(new Rect(0, yPos, position.width, 220),
                    @"This tool will automatically add or correct the namespaces on any C# scripts in your project, making them consistent with your SmartNS settings.

BE CAREFUL!

This is a potentially destrucive tool. It will modify the actual file contents on your script, possibly incorrectly. There is no 'Undo'. Don't use this tool unless you have an easy way to revert the changes it makes, such as version control.

See the Documentation.txt file for more information on this. But in general, you probably shouldn't run this on 3rd-party code you got from the asset store.");

            yPos += 220;

            GUI.Box(new Rect(0, yPos, position.width, 100), @"Instructions:
 - Click the 'Base Directory' button to choose the base directory. Only scripts in, or under, that directory will be processed.
 - The click 'Begin Namespace Conversion'
 - Look in the Unity Console log for errors and other information on the progress.");

            yPos += 100;

            var baseDirectoryLabel = new GUIContent(string.Format("Base Directory: {0}", _baseDirectory),
                                                    "SmartNS will search all scripts in, or below, this directory. Use this to limit the search to a subdirectory.");

            if (GUI.Button(new Rect(3, yPos, position.width - 6, 20), baseDirectoryLabel)) {
                var fullPath = EditorUtility.OpenFolderPanel("Choose root folder", _baseDirectory, "");
                _baseDirectory = fullPath.Replace(Application.dataPath, "Assets").Trim();
                if (string.IsNullOrWhiteSpace(_baseDirectory)) {
                    _baseDirectory = "Assets";
                }
            }

            yPos += 30;

            if (!_isProcessing) {

                var submitButtonContent = new GUIContent("Begin Namespace Conversion", "Begin processing scripts");
                var submitButtonStyle = new GUIStyle(GUI.skin.button);
                submitButtonStyle.normal.textColor = new Color(0, .5f, 0);

                if (GUI.Button(new Rect(position.width / 2 - 350 / 2, yPos, 350, 30), submitButtonContent,
                               submitButtonStyle)) {
                    string assetBasePath =
                        (string.IsNullOrWhiteSpace(_baseDirectory) ? "Assets" : _baseDirectory).Trim();
                    if (!assetBasePath.EndsWith("/")) {
                        assetBasePath += "/";
                    }

                   
                    _assetsToProcess = GetAssetsToProcess(assetBasePath);

                    // foreach (var t in GetAssetsToProcessAsmdefFiles(assetBasePath)) {
                    //     Debug.LogError(t);
                    // }
                    ProcessRenameAllAsmdefFiles();


                    if (EditorUtility.DisplayDialog("Are you sure?",
                                                    string.Format(
                                                        "This will process a total of {0} scripts found in or under the '{1}' directory, updating their namespaces based on your current SmartNS settings. You should back up your project before doing this, in case something goes wrong. Are you sure you want to do this?",
                                                        _assetsToProcess.Count, assetBasePath),
                                                    string.Format("I'm sure. Process {0} scripts",
                                                                  _assetsToProcess.Count),
                                                    "Cancel")) {

                        var smartNSSettings = SmartNSSettings.GetSerializedSettings();
                        _scriptRootSettingsValue = smartNSSettings.FindProperty("_ScriptRoot").stringValue;
                        _prefixSettingsValue = smartNSSettings.FindProperty("_NamespacePrefix").stringValue;
                        _universalNamespaceSettingsValue =
                            smartNSSettings.FindProperty("_UniversalNamespace").stringValue;
                        _useSpacesSettingsValue = smartNSSettings.FindProperty("_IndentUsingSpaces").boolValue;
                        _numberOfSpacesSettingsValue = smartNSSettings.FindProperty("_NumberOfSpaces").intValue;
                        _directoryDenyListSettingsValue =
                            smartNSSettings.FindProperty("_DirectoryIgnoreList").stringValue;
                        _enableDebugLogging = smartNSSettings.FindProperty("_EnableDebugLogging").boolValue;
                        _ignoredDirectories = SmartNS.GetIgnoredDirectories();
                        _progressCount = 0;
                        _isProcessing = true;
                    }
                }
            }

            if (_isProcessing) {
                var cancelButtonContent = new GUIContent("Cancel", "Cancel script conversion");
                var cancelButtonStyle = new GUIStyle(GUI.skin.button);
                cancelButtonStyle.normal.textColor = new Color(.5f, 0, 0);
                if (GUI.Button(new Rect(position.width / 2 - 50 / 2, yPos, 50, 30), cancelButtonContent,
                               cancelButtonStyle)) {
                    _isProcessing = false;
                    _progressCount = 0;
                    AssetDatabase.Refresh();
                    Log("Cancelled");
                }

                yPos += 40;

                if (_progressCount < _assetsToProcess.Count) {
                    EditorGUI.ProgressBar(new Rect(3, yPos, position.width - 6, 20),
                                          (float) _progressCount / (float) _assetsToProcess.Count,
                                          string.Format("Processing {0} ({1}/{2})", _assetsToProcess[_progressCount],
                                                        _progressCount, _assetsToProcess.Count));
                    Log("Processing " + _assetsToProcess[_progressCount]);

                    SmartNS.UpdateAssetNamespace(_assetsToProcess[_progressCount],
                                                 _scriptRootSettingsValue,
                                                 _prefixSettingsValue,
                                                 _universalNamespaceSettingsValue,
                                                 _useSpacesSettingsValue,
                                                 _numberOfSpacesSettingsValue,
                                                 _directoryDenyListSettingsValue,
                                                 _enableDebugLogging,
                                                 directoryIgnoreList: _ignoredDirectories);

                    _progressCount++;
                }
                else {
                    // We done. 
                    _isProcessing = false;
                    _ignoredDirectories = null;
                    _progressCount = 0;
                    AssetDatabase.Refresh();
                    Debug.Log("Bulk Namespace Conversion complete.");
                }
            }

        }

        private List<string> GetAssetsToProcess(string assetBasePath) {
            var ignoredDirectories = SmartNS.GetIgnoredDirectories();

            Func<string, bool> isInIgnoredDirectory = (assetPath) => {
                var indexOfAsset = Application.dataPath.LastIndexOf("Assets");
                var fullFilePath = Application.dataPath.Substring(0, indexOfAsset) + assetPath;
                var fileInfo = new FileInfo(fullFilePath);
                return ignoredDirectories.Contains(fileInfo.Directory.FullName);

            };

            return AssetDatabase.GetAllAssetPaths()
                                .Where(s => s.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
                                            && s.StartsWith("Assets", StringComparison.OrdinalIgnoreCase)
                                            && s.StartsWith(assetBasePath, StringComparison.OrdinalIgnoreCase)
                                            && !isInIgnoredDirectory(s)).ToList();
        }

        private List<string> GetAssetsToProcessAsmdefFiles(string assetBasePath)
        {
            var ignoredDirectories = SmartNS.GetIgnoredDirectories();

            Func<string, bool> isInIgnoredDirectory = (assetPath) => {
                var indexOfAsset = Application.dataPath.LastIndexOf("Assets");
                var fullFilePath = Application.dataPath.Substring(0, indexOfAsset) + assetPath;
                var fileInfo = new FileInfo(fullFilePath);
                return ignoredDirectories.Contains(fileInfo.Directory.FullName);

            };

            return AssetDatabase.GetAllAssetPaths()
                                .Where(s => s.EndsWith(".asmdef", StringComparison.OrdinalIgnoreCase)
                                            && s.StartsWith("Assets", StringComparison.OrdinalIgnoreCase)
                                            && s.StartsWith(assetBasePath, StringComparison.OrdinalIgnoreCase)
                                            && !isInIgnoredDirectory(s)).ToList();
        }

        void Update() {
            if (_isProcessing) {
                Repaint();
            }
        }

        private void Log(string message) {
            Debug.Log(string.Format("[SmartNS] {0}", message));
        }
    }

}
