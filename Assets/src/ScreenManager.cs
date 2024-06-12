using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        UpdateScreen();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateScreen();
        }
    }

    void UpdateScreen()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Screen.width > Screen.height)
            {
                HandleLandscapeMode();
            }
            else
            {
                HandlePortraitMode();
            }
        }
        else
        {
            HandleDesktopMode();
        }

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }

    void HandleLandscapeMode()
    {
        Debug.Log("Landscape mode");
    }

    void HandlePortraitMode()
    {
        Debug.Log("Portrait mode");
    }

    void HandleDesktopMode()
    {
        Debug.Log("Desktop mode");
    }
}
