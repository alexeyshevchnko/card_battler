using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Loader : MonoBehaviour
{
    [SerializeField] private string _battleWebglMobileName = "Scenes/battle_webgl_mobile";
    [SerializeField] private string _battleWebglDesktopName = "Scenes/battle_webgl_desktop";
    void Start()
    {
        LoadSceneBasedOnDevice();
    }

    void LoadSceneBasedOnDevice()
    {
        string sceneAddress;

        if (IsMobileDevice())
        {
            sceneAddress = _battleWebglMobileName;
        }
        else
        {
            sceneAddress = _battleWebglDesktopName;
        }

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