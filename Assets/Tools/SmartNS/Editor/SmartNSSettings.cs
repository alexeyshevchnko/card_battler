using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine; 

#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#elif UNITY_2018_4_OR_NEWER
using UnityEngine.Experimental.UIElements;
#endif

namespace Tools.SmartNS.Editor{
    public class SmartNSSettings : ScriptableObject {
        public const string SCRIPT_ROOT_TOOLTIP =
            "Whatever you place here will be stripped off the beginning of the namespace. Normally this should be 'Assets', as Unity will automatically place new scripts in '/Assets'. But if you keep all your scripts in 'Assets/Code', you could out 'Assets/Code' here to strip that out of the namespace. Note that any scripts created at the level of the Script Root will not be given a namespace, unless Universal namespacing is used.";

        public const string NAMESPACE_PREFIX_TOOLTIP =
            "This will be added to the beginning of the namespace. This is useful for placing the project or company name in your namespace.";

        public const string UNIVERSAL_NAMESPACE_TOOLTIP =
            "Instead of using the 'Smart' functionality, based on the current directory, this will place all code into the same namespace you specify here.";

        public const string INDENT_USING_SPACES_TOOLTIP = "Enables the use of spaces for indentation instead of tabs.";
        public const string NUMBER_OF_SPACES_TOOLTIP = "How many spaces to use per indentation level.";

        public const string UPDATE_NAMESPACES_WHEN_MOVING_SCRIPTS_TOOLTIP =
            "(Experimental) When exabled, SmartNS will run on any scripts you move within your project, updating their namespaces. Note: This doesn't work when moving directories that contain scripts.";

        public const string DIRECTORY_IGNORE_LIST_TOOLTIP =
            "(Experimental) Prevents SmartNS from acting on any scripts located within the following directories, or any child directories. Useful for preventing SmartNS from acting on certain directories, such as where you keep 3rd-party assets.";

        public const string ENABLE_DEBUG_LOGGING_TOOLTIP =
            "This turns on some extra logging for SmartNS. Not usually interesting to anyone but the developer.";

#pragma warning disable 0414
        [SerializeField] private string _ScriptRoot;
        [SerializeField] private string _NamespacePrefix;
        [SerializeField] private string _UniversalNamespace;
        [SerializeField] private bool _IndentUsingSpaces;
        [SerializeField] private int _NumberOfSpaces;
        [SerializeField] private bool _UpdateNamespacesWhenMovingScripts;
        [SerializeField] private string _DirectoryIgnoreList;
        [SerializeField] private bool _EnableDebugLogging; 
#pragma warning restore 0414

        private const string _defaultSmartNSSettingsDirectoryPath = "Assets/SmartNS";
        private const string _defaultSmartNSSettingsAssetName = "SmartNSSettings.asset";

        internal static SmartNSSettings GetOrCreateSettings() {
            var smartNSSettings = GetSmartNSSettingsAsset();

            if (smartNSSettings == null) { 
                smartNSSettings = ScriptableObject.CreateInstance<SmartNSSettings>();
                smartNSSettings._ScriptRoot = "Assets";
                smartNSSettings._NamespacePrefix = "";
                smartNSSettings._UniversalNamespace = "";
                smartNSSettings._IndentUsingSpaces = true;
                smartNSSettings._NumberOfSpaces = 4; 
                smartNSSettings._UpdateNamespacesWhenMovingScripts = false;
                smartNSSettings._DirectoryIgnoreList = "";
                smartNSSettings._EnableDebugLogging = false;

                string fullAssetPath = "";
                if (AssetDatabase.IsValidFolder(_defaultSmartNSSettingsDirectoryPath)) {
                    fullAssetPath = Path.Combine(_defaultSmartNSSettingsDirectoryPath,
                                                 _defaultSmartNSSettingsAssetName);
                }
                else {
                    fullAssetPath = Path.Combine("Assets", _defaultSmartNSSettingsAssetName);
                }

                AssetDatabase.CreateAsset(smartNSSettings, fullAssetPath);
                AssetDatabase.SaveAssets();
            }

            return smartNSSettings;
        }

        internal static SerializedObject GetSerializedSettings() {
            return new SerializedObject(GetOrCreateSettings());
        }

        public static bool SettingsFileExists() {
            return GetSmartNSSettingsAsset() != null;
        }

        private static string GetSettingsFilePath() {
            var smartNSSettingsAssetGuids = AssetDatabase.FindAssets("t:SmartNSSettings");

            if (smartNSSettingsAssetGuids.Length > 1) {
                var paths = string.Join(
                    ", ", smartNSSettingsAssetGuids.Select(guid => AssetDatabase.GUIDToAssetPath(guid)));
                Debug.LogWarning(string.Format(
                                     "Multiple SmartNSSettings.asset files exist in this project. This may lead to confusion, as any of the settings files may be chosen arbitrarily. You should remove all but one of the following so that you only have one SmartNSSettings.asset files: {0}",
                                     paths));
            }

            if (smartNSSettingsAssetGuids.Length > 0) {
                return AssetDatabase.GUIDToAssetPath(smartNSSettingsAssetGuids.First());
            }

            return null;
        }
 

        public static SmartNSSettings GetSmartNSSettingsAsset() {
            SmartNSSettings smartNSSettings = null;
            var smartNSSettingsAssetGuids = AssetDatabase.FindAssets("t:SmartNSSettings");

            if (smartNSSettingsAssetGuids.Length > 1) {
                var paths = string.Join(
                    ", ", smartNSSettingsAssetGuids.Select(guid => AssetDatabase.GUIDToAssetPath(guid)));
                Debug.LogWarning(string.Format(
                                     "Multiple SmartNSSettings.asset files exist in this project. This may lead to confusion, as any of the settings files may be chosen arbitrarily. You should remove all but one of the following so that you only have one SmartNSSettings.asset files: {0}",
                                     paths));
            }

            if (smartNSSettingsAssetGuids.Length > 0) {
                smartNSSettings =
                    AssetDatabase.LoadAssetAtPath<SmartNSSettings>(
                        AssetDatabase.GUIDToAssetPath(smartNSSettingsAssetGuids.First()));
            }

            var settingsFilePath = GetSettingsFilePath();
            if (settingsFilePath == null) {
                return null;
            }
            else {
                return AssetDatabase.LoadAssetAtPath<SmartNSSettings>(settingsFilePath);
            }
        }
    }

    public class SmartNSSettingsProvider : SettingsProvider {
        private SerializedObject _SmartNSSettings;
        private Vector2 _ignoreListScrollPos;

        class Styles {
            public static GUIContent ScriptRoot = new GUIContent("Script Root", SmartNSSettings.SCRIPT_ROOT_TOOLTIP);

            public static GUIContent NamespacePrefix =
                new GUIContent("Namespace Prefix", SmartNSSettings.NAMESPACE_PREFIX_TOOLTIP);

            public static GUIContent UniversalNamespace =
                new GUIContent("Universal Namespace", SmartNSSettings.UNIVERSAL_NAMESPACE_TOOLTIP);

            public static GUIContent IndentUsingSpaces =
                new GUIContent("Indent using Spaces", SmartNSSettings.INDENT_USING_SPACES_TOOLTIP);

            public static GUIContent NumberOfSpaces =
                new GUIContent("Number of Spaces", SmartNSSettings.NUMBER_OF_SPACES_TOOLTIP);

            public static GUIContent UpdateNamespacesWhenMovingScripts = new GUIContent(
                "Update Namespaces When Moving Scripts", SmartNSSettings.UPDATE_NAMESPACES_WHEN_MOVING_SCRIPTS_TOOLTIP);

            public static GUIContent DirectoryIgnoreList =
                new GUIContent("Directory Deny List (One directory per line)",
                               SmartNSSettings.DIRECTORY_IGNORE_LIST_TOOLTIP);

            public static GUIContent EnableDebugLogging =
                new GUIContent("Enable Debug Logging", SmartNSSettings.ENABLE_DEBUG_LOGGING_TOOLTIP);
        }

        public SmartNSSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope) { }

        public static bool IsSettingsAvailable() {
            return SmartNSSettings.SettingsFileExists();
        }

        public override void OnActivate(string searchContext, VisualElement rootElement) {
            _SmartNSSettings = SmartNSSettings.GetSerializedSettings();
        }

        public override void OnDeactivate() {
            AssetDatabase.SaveAssets();
        }

        public override void OnGUI(string searchContext) {
            _SmartNSSettings.Update();

            EditorGUILayout.LabelField(string.Format("Version {0}", SmartNS.SmartNSVersionNumber));

            EditorGUILayout.HelpBox(
                "SmartNS adds a namespace to new C# scripts based on the directory in which they are created. Optionally, a 'Universal' namespace can be used for all scripts.",
                MessageType.None);
            EditorGUIUtility.labelWidth = 245.0f;

            EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("_ScriptRoot"), Styles.ScriptRoot);
            EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("_NamespacePrefix"), Styles.NamespacePrefix);
            EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("_UniversalNamespace"),
                                          Styles.UniversalNamespace);
            var useSpacesProperty = _SmartNSSettings.FindProperty("_IndentUsingSpaces");
            var useSpaces = EditorGUILayout.PropertyField(useSpacesProperty, Styles.IndentUsingSpaces);
            if (useSpacesProperty.boolValue) {
                EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("_NumberOfSpaces"),
                                              Styles.NumberOfSpaces);
            }

            EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("_EnableDebugLogging"),
                                          Styles.EnableDebugLogging);

            EditorGUILayout.Space();
            var boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("Experimental", boldStyle);
            EditorGUILayout.PropertyField(_SmartNSSettings.FindProperty("m_UpdateNamespacesWhenMovingScripts"),
                                          Styles.UpdateNamespacesWhenMovingScripts);

            EditorGUILayout.LabelField(Styles.DirectoryIgnoreList);
            _ignoreListScrollPos = EditorGUILayout.BeginScrollView(_ignoreListScrollPos, GUILayout.Height(120));
            _SmartNSSettings.FindProperty("_DirectoryIgnoreList").stringValue =
                EditorGUILayout.TextArea(_SmartNSSettings.FindProperty("_DirectoryIgnoreList").stringValue);
            EditorGUILayout.EndScrollView();


            _SmartNSSettings.ApplyModifiedProperties();
        }

        [SettingsProvider]
        public static SettingsProvider CreateSmartNSSettingsProvider() {
            if (!IsSettingsAvailable()) {
                SmartNSSettings.GetOrCreateSettings();
            }


            var provider = new SmartNSSettingsProvider("Project/SmartNS", SettingsScope.Project);

            provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;

        }
    }

}
