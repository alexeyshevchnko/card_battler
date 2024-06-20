using System.Threading;
using UnityEngine;

namespace Shared.Test{
    public class FPS : MonoBehaviour {

        private delegate void DelayDelegate();
        private float fps;
        private float avarage = 5;
        private float max = 0;
        private float min = 100;
        private string showString = "";
        private float lastFrame;
        private float localMax;
        private float localMin;
        private int localFPS = 0;
        private GUISkin _skin;

        [SerializeField] int _targetFrameRate = 60;
        [SerializeField] bool _enabledFPS = true;
        [SerializeField] int _simulateDelay = 0;

        private DelayDelegate SleepOnUpdate = () => { };

        public float Value {
            get { return fps; }
        }

        void Awake() {
            Application.targetFrameRate = _targetFrameRate;
            enabled = _enabledFPS;
            if (_simulateDelay > 0)
                SleepOnUpdate = Sleep;
        }

        private GUISkin GetSkin(GUISkin prefab) {
            var skin = Instantiate(prefab);
            skin.label.fontSize = 20;
            skin.label.wordWrap = false;

            return skin;
        }

        private void Sleep() {
            Thread.Sleep(_simulateDelay);
            //Debug.Log("Sleep at " + Time.time);
        }

        void Update() {
            float thisFrame = Time.realtimeSinceStartup;
            float deltaTime = thisFrame - lastFrame;
            if ((int) thisFrame > lastFrame)
                UpdateMax();
            lastFrame = thisFrame;

            localFPS++;
            if (localMax < deltaTime)
                localMax = deltaTime;
            if (localMin > deltaTime)
                localMin = deltaTime;

            SleepOnUpdate();
        }

        private void UpdateMax() {
            fps = localFPS;
            avarage = 1f / fps;
            localFPS = 0;

            max = localMax;
            localMax = 0;

            min = localMin;
            localMin = float.MaxValue;

            showString = "fps:" + fps.ToString("0.0") +
                         " frame:" + (avarage * 1000).ToString("0.0") +
                         " [" + (min * 1000).ToString("0.0") +
                         ", " + (max * 1000).ToString("0.0") + "]";
        }

        void OnGUI() {
            if (_skin == null) {
                _skin = GUI.skin;
                _skin.label.fontSize = Screen.width / 25;
            }

            var content = new GUIContent(showString);
            var size = _skin.label.CalcSize(content);
            size.x += _skin.box.margin.horizontal;
            GUILayout.BeginVertical("box", GUILayout.MinWidth(size.x));
            GUILayout.Label(showString);
            GUILayout.EndVertical();
        }
    }

}
