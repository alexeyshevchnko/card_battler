using System.Collections.Generic;
using UnityEngine;

namespace Shared.Test{

    class Console : MonoBehaviour {
        public static Console Instance;

        struct Log {
            public string message;
            public string stackTrace;
            public LogType type;
        }

        #region Inspector Settings

#pragma warning disable CS0649 // Field is never assigned
        [SerializeField] LogType _logLevel;
#pragma warning restore CS0649 // Field is never assigned

        /// <summary>
        /// The hotkey to show and hide the console window.
        /// </summary>
        public KeyCode toggleKey = KeyCode.BackQuote;

        /// <summary>
        /// Whether to open the window by shaking the device (mobile-only).
        /// </summary>
        public bool shakeToOpen = true;

        /// <summary>
        /// The (squared) acceleration above which the window should open.
        /// </summary>
        public float shakeAcceleration = 3f;

        /// <summary>
        /// Whether to only keep a certain number of logs.
        ///
        /// Setting this can be helpful if memory usage is a concern.
        /// </summary>
        public bool restrictLogCount = false;

        /// <summary>
        /// Number of logs to keep before removing old ones.
        /// </summary>
        public int maxLogs = 100;

        #endregion

        readonly List<Log> logs = new List<Log>();
        Vector2 scrollPosition;
        bool visible;
        bool collapse;
        bool callStack = false;

        // Visual elements:

        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color> {
            {LogType.Assert, Color.white},
            {LogType.Error, Color.red},
            {LogType.Exception, Color.red},
            {LogType.Log, Color.white},
            {LogType.Warning, Color.yellow},
        };

        const string windowTitle = "Console";
        const int margin = 20;
        const float controlSize = 0.03f; // 3% from width/height
        static readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        static readonly GUIContent closeLabel = new GUIContent("Close", "Close the console.");
        static readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        static readonly GUIContent callStackLabel = new GUIContent("CallStack", "Shows call stack.");

        readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);
        Rect windowRect = new Rect(margin, margin, Screen.width - (margin * 2), Screen.height - (margin * 2));
        GUISkin customSkin;

        private void Awake() {
            Instance = this;
#if !UNITY_EDITOR
            Debug.unityLogger.filterLogType = _logLevel;
#endif
        }

        public static void SetVisibility(bool isVisible) {
            Instance.visible = isVisible;
        }

        void OnEnable() {
#if UNITY_5 || UNITY_2018 || UNITY_2019 || UNITY_2020
            Application.logMessageReceived += HandleLog;
#else
            Application.RegisterLogCallback(HandleLog);
#endif
        }

        void OnDisable() {
#if UNITY_5 || UNITY_2018 || UNITY_2019 || UNITY_2020
            Application.logMessageReceived -= HandleLog;
#else
            Application.RegisterLogCallback(null);
#endif
        }

        void Update() {
            if (Input.GetKeyDown(toggleKey)) {
                visible = !visible;
            }

            if (shakeToOpen && Input.acceleration.sqrMagnitude > shakeAcceleration) {
                visible = true;
            }
        }

        void OnGUI() {
            SetSkin();

            if (visible) {
                windowRect = GUILayout.Window(123456, windowRect, DrawConsoleWindow, windowTitle);
            }
            else {
                float size = Screen.width * 0.1f;
                if (GUI.Button(new Rect(Screen.width - size, 0, size, size), ""))
                    visible = true;
            }
        }

        private void SetSkin() {
            if (customSkin == null) {
                customSkin = GUI.skin;
                customSkin.label.fontSize = Screen.width / 25;
                float barWidth = windowRect.width * controlSize;
                customSkin.verticalScrollbarThumb.fixedWidth = barWidth;
                customSkin.verticalScrollbar.fixedWidth = barWidth;
            }

            GUI.skin = customSkin;
            GUI.backgroundColor = Color.black;
        }

        /// <summary>
        /// Displays a window that lists the recorded logs.
        /// </summary>
        /// <param name="windowID">Window ID.</param>
        void DrawConsoleWindow(int windowID) {
            DrawToolbar();
            DrawLogsList();

            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(titleBarRect);
        }

        /// <summary>
        /// Displays a scrollable list of logs.
        /// </summary>
        void DrawLogsList() {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Iterate through the recorded logs.
            for (var i = 0; i < logs.Count; i++) {
                var log = logs[i];

                // Combine identical messages if collapse option is chosen.
                if (collapse && i > 0) {
                    var previousMessage = logs[i - 1].message;

                    if (log.message == previousMessage) {
                        continue;
                    }
                }

                GUI.contentColor = logTypeColors[log.type];
                GUILayout.Label(log.message + (callStack ? "\n" + log.stackTrace : ""));
            }

            for (int i = 0; i < 10; i++)
                GUILayout.Label("   "); // space at the end if banner over message list

            GUILayout.EndScrollView();

            // Ensure GUI colour is reset before drawing other components.
            GUI.contentColor = Color.white;
        }

        /// <summary>
        /// Displays options for filtering and changing the logs list.
        /// </summary>
        void DrawToolbar() {
            GUILayoutOption minHeightOpt = GUILayout.MinHeight(windowRect.height * controlSize);
            GUILayout.BeginHorizontal(minHeightOpt);

            if (GUILayout.Button(clearLabel, minHeightOpt)) {
                logs.Clear();
            }

            GUILayout.Space(windowRect.width * controlSize);
            collapse = GUILayout.Toggle(collapse, collapseLabel, minHeightOpt);
            callStack = GUILayout.Toggle(callStack, callStackLabel, minHeightOpt);

            if (GUILayout.Button(closeLabel, minHeightOpt)) {
                visible = false;
            }

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Records a log from the log callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="stackTrace">Trace of where the message came from.</param>
        /// <param name="type">Type of message (error, exception, warning, assert).</param>
        void HandleLog(string message, string stackTrace, LogType type) {
            logs.Add(new Log {
                message = message,
                stackTrace = stackTrace,
                type = type,
            });

            TrimExcessLogs();
        }

        /// <summary>
        /// Removes old logs that exceed the maximum number allowed.
        /// </summary>
        void TrimExcessLogs() {
            if (!restrictLogCount) {
                return;
            }

            var amountToRemove = Mathf.Max(logs.Count - maxLogs, 0);

            if (amountToRemove == 0) {
                return;
            }

            logs.RemoveRange(0, amountToRemove);
        }
    }

}