using Game.Data.Battle;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game{
    
    public class Loader : MonoBehaviour
    {
        [SerializeField] private string _sceneBattleWebglMobile = "Assets/Scenes/battle_webgl_mobile.unity";
        [SerializeField] private string _scenesBattleWebglDesktop = "Assets/Scenes/battle_webgl_desktop.unity";
    
        void Start()
        {
            //  LoadSceneBasedOnDevice();

            //TODO
            var generateConfigs = new GenerateConfigs();
            Debug.Log($" test configs  {generateConfigs.EnemyCardAction.CardActionList[0].GetName()}");
        }
    
        void LoadSceneBasedOnDevice()
        {
            string sceneAddress = IsMobileDevice() ? _sceneBattleWebglMobile : _scenesBattleWebglDesktop;

            
            Addressables.LoadSceneAsync(sceneAddress).Completed += OnSceneLoaded;
        }
        
        bool IsMobileDevice()
        {
            return Application.isMobilePlatform || (Application.platform == RuntimePlatform.IPhonePlayer) || (Application.platform == RuntimePlatform.Android);
        }
        
        void OnSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Scene loaded successfully!");
            }
            else
            {
                Debug.LogError("Failed to load scene.");
            }
        }
         
    }
}