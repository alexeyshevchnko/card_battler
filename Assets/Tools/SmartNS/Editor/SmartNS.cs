using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Tools.SmartNS.Editor
{
    public class SmartNS : UnityEditor.AssetModificationProcessor {
        public const string SmartNSVersionNumber = "2.0.2"; 
        private static string PathSeparator = "/";

        private static string ByteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        private static bool _shouldWriteDebugLogInfo = false;

        #region Asset Creation 
        public static void OnWillCreateAsset(string path) {
            try {
                if (!path.EndsWith(".cs.meta")) {
                    return;
                }


                path = path.Replace(".meta", "");
                path = path.Trim();
                int index = path.LastIndexOf(".");
                if (index < 0) {
                    return;
                }

                var smartNSSettings = SmartNSSettings.GetSerializedSettings();
                var enableDebugLogging = smartNSSettings.FindProperty("_EnableDebugLogging").boolValue;
                _shouldWriteDebugLogInfo = enableDebugLogging;

                ProcessNamespaceForScriptAtPath(path);

            }
            catch (Exception ex) {
                Debug.LogError(string.Format("Something went really wrong trying to execute SmartNS: {0}", ex.Message));
                Debug.LogError(string.Format("SmartNS Failure Stack Trace: {0}", ex.StackTrace));
            }
        }
        #endregion

        #region Asset Moved
        private static HashSet<string> _currentlyMovingAssets = new HashSet<string>();

        public static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath) {
            var assetMoveResult = AssetMoveResult.DidNotMove;

            try {
                if (!_currentlyMovingAssets.Contains(sourcePath)) {
                    if (!SmartNSSettings.SettingsFileExists()) {
                        SmartNSSettings.GetOrCreateSettings();
                    }

                    var smartNSSettings = SmartNSSettings.GetSerializedSettings();
                    var updateNamespacesWhenMovingScripts =
                        smartNSSettings.FindProperty("_UpdateNamespacesWhenMovingScripts").boolValue;
                    var enableDebugLogging = smartNSSettings.FindProperty("_EnableDebugLogging").boolValue;
                    _shouldWriteDebugLogInfo = enableDebugLogging;

                    if (updateNamespacesWhenMovingScripts) {
                        if (sourcePath.EndsWith(".cs")) {
                            WriteDebug("Moving asset. Source path: " + sourcePath + ". Destination path: " + destinationPath + "."); 
                            var validationMessage = AssetDatabase.ValidateMoveAsset(sourcePath, destinationPath);

                            if (string.IsNullOrWhiteSpace(validationMessage)) {
                                _currentlyMovingAssets.Add(sourcePath);
                                AssetDatabase.MoveAsset(sourcePath, destinationPath);
                                ProcessNamespaceForScriptAtPath(destinationPath);
                                assetMoveResult = AssetMoveResult.DidMove;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Debug.LogError(string.Format("Something went really wrong trying to execute SmartNS: {0}", ex.Message));
                Debug.LogError(string.Format("SmartNS Failure Stack Trace: {0}", ex.StackTrace));
            }
            finally {
                _currentlyMovingAssets.Remove(sourcePath);
            }

            return assetMoveResult;
        }

        #endregion



        public static void ProcessNamespaceForScriptAtPath(string path, HashSet<string> directoryIgnoreList = null) {
            if (!SmartNSSettings.SettingsFileExists()) {
                SmartNSSettings.GetOrCreateSettings();
            }

            var smartNSSettings = SmartNSSettings.GetSerializedSettings();
            var scriptRootSettingsValue = smartNSSettings.FindProperty("_ScriptRoot").stringValue;
            var prefixSettingsValue = smartNSSettings.FindProperty("_NamespacePrefix").stringValue;
            var universalNamespaceSettingsValue = smartNSSettings.FindProperty("_UniversalNamespace").stringValue;
            var useSpacesSettingsValue = smartNSSettings.FindProperty("_IndentUsingSpaces").boolValue;
            var numberOfSpacesSettingsValue = smartNSSettings.FindProperty("_NumberOfSpaces").intValue;
            var directoryDenyListSettingsValue = smartNSSettings.FindProperty("_DirectoryIgnoreList").stringValue;
            var enableDebugLogging = smartNSSettings.FindProperty("_EnableDebugLogging").boolValue;

            UpdateAssetNamespace(path,
                                 scriptRootSettingsValue,
                                 prefixSettingsValue,
                                 universalNamespaceSettingsValue,
                                 useSpacesSettingsValue,
                                 numberOfSpacesSettingsValue,
                                 directoryDenyListSettingsValue,
                                 enableDebugLogging,
                                 directoryIgnoreList);

            AssetDatabase.ImportAsset(path);
        }

        internal static void UpdateAssetNamespace(string assetPath,
                                                  string scriptRootSettingsValue,
                                                  string prefixSettingsValue,
                                                  string universalNamespaceSettingsValue,
                                                  bool useSpacesSettingsValue,
                                                  int numberOfSpacesSettingsValue,
                                                  string directoryDenyListSettingsValue,
                                                  bool enableDebugLogging,
                                                  HashSet<string> directoryIgnoreList = null) {

            _shouldWriteDebugLogInfo = enableDebugLogging;

            WriteDebug(string.Format("Acting on new C# file: {0}", assetPath));
            var indexOfAsset = Application.dataPath.LastIndexOf("Assets");
            var fullFilePath = Application.dataPath.Substring(0, indexOfAsset) + assetPath;
            var fileInfo = new FileInfo(fullFilePath);
            WriteDebug(string.Format("Full Path: {0}", fullFilePath));
            if (!System.IO.File.Exists(fullFilePath)) {
                WriteDebug(string.Format("Path doesn't exist: {0}. Exiting.", fullFilePath));
                return;
            }

            if (directoryIgnoreList == null) {
                directoryIgnoreList = GetIgnoredDirectories();
                if (directoryIgnoreList.Contains(fileInfo.Directory.FullName)) {
                    WriteDebug(string.Format("File {0} is a child of an ignored directory. Exiting.", fullFilePath));
                    return;
                }
            } 

            string namespaceValue = GetNamespaceValue(assetPath, scriptRootSettingsValue, prefixSettingsValue,
                                                      universalNamespaceSettingsValue);

            //Debug.LogError($"assetPath = {assetPath} namespaceValue = {namespaceValue}");

            if (namespaceValue == null) {
                WriteDebug(string.Format("Generated namespace for {0} is null. Exiting.", fullFilePath));
                return;
            }

            var lineEnding = DetectLineEndings(fullFilePath);
             
            string rawFileContents = System.IO.File.ReadAllText(fullFilePath);
            string[] rawLines = rawFileContents.Split(new[] {lineEnding}, StringSplitOptions.None);


            if (rawLines.Length == 0) {
                WriteDebug(string.Format("File {0} contains no lines. Exiting.", fullFilePath));
                return;
            }


            var modifiedLines = new List<string>(); 
            bool hasExistingNamespace = false;
            for (int rawLineIndex = 0; rawLineIndex < rawLines.Length; rawLineIndex++) {
                var line = rawLines[rawLineIndex];
                if (!hasExistingNamespace && line.TrimStart().StartsWith("namespace ")) {
                    hasExistingNamespace = true;

                    WriteDebug(string.Format("Existing namespace found on line index {0}", rawLineIndex)); 
                    while (rawLineIndex < rawLines.Length) {
                        if (line.Contains("{")) {
                            var indexOfCurlyBrace = line.IndexOf('{');
                            var curlyBraceOnward = line.Substring(indexOfCurlyBrace);
                            if (indexOfCurlyBrace == 0) {
                                modifiedLines.Add(string.Format("namespace {0}", namespaceValue));
                                modifiedLines.Add(curlyBraceOnward);
                            }
                            else {
                                modifiedLines.Add(string.Format("namespace {0}{1}", namespaceValue, curlyBraceOnward));
                            }

                            break;
                        }
                        else {
                            rawLineIndex++;
                            line = rawLines[rawLineIndex];
                        }
                    }
                }
                else {
                    modifiedLines.Add(line);
                }
            }

            if (!hasExistingNamespace) {

                var lastUsingLineIndex = 0;

                for (var i = modifiedLines.Count - 1; i >= 0; i--) {
                    if (modifiedLines[i].StartsWith("using ")) {
                        lastUsingLineIndex = i;
                        break;
                    }
                }

                modifiedLines.Insert(lastUsingLineIndex + 1, "");
                modifiedLines.Insert(lastUsingLineIndex + 2, string.Format("namespace {0} {{", namespaceValue));


                for (var i = lastUsingLineIndex + 3; i < modifiedLines.Count; i++) {
                    var prefix = useSpacesSettingsValue
                        ? new string(' ', numberOfSpacesSettingsValue)
                        : "\t";
                    modifiedLines[i] = string.Format("{0}{1}", prefix, modifiedLines[i]);
                }

                modifiedLines.Add("}");
            }

            var newFileContents = string.Join(lineEnding, modifiedLines.ToArray());

            if (rawFileContents.EndsWith(Environment.NewLine) || rawFileContents.EndsWith(lineEnding)) {
                if (!(newFileContents.EndsWith(Environment.NewLine) || newFileContents.EndsWith(lineEnding))) {
                    newFileContents += lineEnding;
                }
            }

            if (rawFileContents.StartsWith(ByteOrderMarkUtf8)) {
                newFileContents = ByteOrderMarkUtf8 + newFileContents;
            }

            File.WriteAllText(fullFilePath, newFileContents);
        }

        private static string DetectLineEndings(string filePath) {
            var allText = System.IO.File.ReadAllText(filePath);

            if (allText.Contains("\r\n")) {
                return "\r\n";
            }
            else if (allText.Contains("\r")) {
                return "\r";
            }
            else if (allText.Contains("\n")) {
                return "\n";
            }
            else {
                return Environment.NewLine;
            }
        }


        public static string GetNamespaceValue(string path, string scriptRootValue, string prefixValue,
                                               string universalNamespaceValue) {
            string namespaceValue = null;

            if (string.IsNullOrEmpty(universalNamespaceValue) || string.IsNullOrEmpty(universalNamespaceValue.Trim())) {
                namespaceValue = path;

                if (scriptRootValue.Trim().Length > 0) {
                    foreach (var scriptRootPathPart in Regex.Split(scriptRootValue.Trim(), PathSeparator)) {
                        var toTrim = scriptRootPathPart.Trim();

                        if (namespaceValue == toTrim || namespaceValue.StartsWith(toTrim + PathSeparator)) {
                            WriteDebug(string.Format("Trimming script root part '{0}' from start of namespace",
                                                     toTrim));
                            namespaceValue = namespaceValue.Substring(toTrim.Length);

                            if (namespaceValue.StartsWith(PathSeparator)) {
                                namespaceValue = namespaceValue.Substring(1);
                            }
                        }
                    }

                }


                var rawPathParts =
                    namespaceValue.Split(PathSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var namespaceParts = rawPathParts.Take(rawPathParts.Count() - 1).ToArray();

                for (int namespacePartIndex = 0; namespacePartIndex < namespaceParts.Length; namespacePartIndex++) {
                    var match = Regex.Match(namespaceParts[namespacePartIndex], "^\\d");
                    if (match.Success) {
                        namespaceParts[namespacePartIndex] = "_" + namespaceParts[namespacePartIndex];
                    }
                }

                namespaceValue = string.Join("/", namespaceParts);
                namespaceValue = namespaceValue.Replace(" ", "");
                namespaceValue = namespaceValue.Replace(".", "_");
                namespaceValue = namespaceValue.Replace("/", ".");
                WriteDebug(string.Format("Script Root = {0}", scriptRootValue));

                if (namespaceValue.StartsWith(".")) {
                    namespaceValue = namespaceValue.Substring(1);
                }

                if (prefixValue.Trim().Length > 0) {
                    if (string.IsNullOrEmpty(namespaceValue.Trim())) {
                        namespaceValue = prefixValue;
                    }
                    else {
                        namespaceValue = string.Format("{0}{1}{2}",
                                                       prefixValue,
                                                       prefixValue.EndsWith(".") ? "" : ".",
                                                       namespaceValue);
                    }

                }

                WriteDebug(string.Format("Using smart namespace: '{0}'", namespaceValue));

            }
            else {
                namespaceValue = universalNamespaceValue.Trim();
                WriteDebug(string.Format("Using 'Universal' namespace: {0}", namespaceValue));
            }

            if (namespaceValue.Trim().Length == 0) {
                return null;
            }

            return namespaceValue;
        }

        public static HashSet<string> GetIgnoredDirectories() {
            var retval = new HashSet<string>();
            var smartNSSettings = SmartNSSettings.GetSerializedSettings();
            var directoryDenyListSettingsValue = smartNSSettings.FindProperty("_DirectoryIgnoreList").stringValue;

            foreach (var directoryPathPart in directoryDenyListSettingsValue.Split(new[] {"\r\n", "\r", "\n"},
                                                                                   StringSplitOptions.RemoveEmptyEntries)) {
                var dpp = directoryPathPart.Trim();
                if (dpp.StartsWith("/")) {
                    dpp = dpp.Remove(0, 1);
                }

                if (!dpp.StartsWith("Assets")) {
                    dpp = "Assets/" + dpp;
                }

                var fullDenyDirectoryPath =
                    Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets")) + dpp;
                var di = new DirectoryInfo(fullDenyDirectoryPath);
                if (di.Exists) {
                    retval.Add(di.FullName);
                    foreach (var subDir in di.GetDirectories("*.*", SearchOption.AllDirectories)) {
                        retval.Add(subDir.FullName);
                    }
                }
                else {
                    Debug.LogWarning(string.Format(
                                         "Directory {0} in SmartNS Project Setting's Directory Ignore List was not a valid directory.",
                                         dpp));
                }
            }

            return retval;
        }

        #region Debug

        public static void WriteDebug(string message) {
            if (_shouldWriteDebugLogInfo) {
                Debug.Log(string.Format("SmartNS Debug: {0}", message));
            }
        }

        #endregion
    }

}
