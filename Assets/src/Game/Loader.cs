using System.Collections;
using Game.Controller;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game {

    public class Loader : MonoBehaviour {
        [SerializeField] private string _sceneBattleWebglMobile = "Assets/Scenes/battle_webgl_mobile.unity";
        [SerializeField] private string _scenesBattleWebglDesktop = "Assets/Scenes/battle_webgl_desktop.unity";

        public static BattleController BattleController => _battleController;
        private static BattleController _battleController;

        IEnumerator Start() {
            yield return TryConnect();
            LoadSceneBasedOnDevice();
        }

        IEnumerator TryConnect() {
            _battleController = new BattleController();
            var result = _battleController.TryConnect(this);
            while (!_battleController.Source.IsConnect) {
                yield return new WaitForSeconds(1);
            }
        }

        void LoadSceneBasedOnDevice() {
            string sceneAddress = IsMobileDevice() ? _sceneBattleWebglMobile : _scenesBattleWebglDesktop;

            Debug.Log("start LoadSceneAsync " + sceneAddress);
            Addressables.LoadSceneAsync(sceneAddress).Completed += OnSceneLoaded;
        }

        bool IsMobileDevice() {
            return Application.isMobilePlatform || (Application.platform == RuntimePlatform.IPhonePlayer) ||
                   (Application.platform == RuntimePlatform.Android);
        }

        void OnSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj) {
            Debug.Log("OnSceneLoaded LoadSceneAsync " + obj);
            if (obj.Status == AsyncOperationStatus.Succeeded) {
                Debug.Log("Scene loaded successfully!");
            }
            else {
                Debug.LogError("Failed to load scene.");
            }
        }
    }
}