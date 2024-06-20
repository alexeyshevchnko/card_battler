namespace Shared.Screen{
    using UnityEngine;

    public class ScreenManager : MonoBehaviour {
    
        private int _lastScreenWidth;
        private int _lastScreenHeight;
    
        void Start() {
            UpdateScreen();
        }
    
        void Update() { 
            if (Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight) {
                UpdateScreen();
            }
        }
    
        void UpdateScreen() {
            if (SystemInfo.deviceType == DeviceType.Handheld) {
                if (Screen.width > Screen.height) {
                    HandleLandscapeMode();
                }
                else {
                    HandlePortraitMode();
                }
            }
            else {
                HandleDesktopMode();
            }
    
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
        }
    
        void HandleLandscapeMode() {
            Debug.Log("Landscape mode");
        }
    
        void HandlePortraitMode() {
            Debug.Log("Portrait mode");
        }
    
        void HandleDesktopMode() {
            Debug.Log("Desktop mode");
        }
    
    }
    
}
