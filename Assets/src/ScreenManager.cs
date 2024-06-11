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
        // Проверка на изменения размера экрана
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateScreen();
        }
    }

    void UpdateScreen()
    {
        // Определение мобильного устройства или десктопа
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            // Мобильное устройство
            if (Screen.width > Screen.height)
            {
                // Пейзажная ориентация
                HandleLandscapeMode();
            }
            else
            {
                // Портретная ориентация
                HandlePortraitMode();
            }
        }
        else
        {
            // Десктопное устройство
            HandleDesktopMode();
        }

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }

    void HandleLandscapeMode()
    {
        // Логика для пейзажной ориентации на мобильных устройствах
        Debug.Log("Landscape mode");
    }

    void HandlePortraitMode()
    {
        // Логика для портретной ориентации на мобильных устройствах
        Debug.Log("Portrait mode");
    }

    void HandleDesktopMode()
    {
        // Логика для десктопного устройства
        Debug.Log("Desktop mode");
    }
}
