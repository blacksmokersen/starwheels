using UnityEngine;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class GraphicsPanelSettings : MonoBehaviour
    {
        // PUBLIC

        public void SetFullScreen(float value)
        {
            if (value == 1)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }

        public void SetScreenResolution(int value)
        {
            switch (value)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case 1:
                    Screen.SetResolution(1366, 768, Screen.fullScreen);
                    break;
                case 2:
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                default:
                    break;
            }
        }

        public void SetGraphicQuality(int value)
        {
            switch (value)
            {
                case 0:
                    QualitySettings.SetQualityLevel(0);
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(1);
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2);
                    break;
                default:
                    break;
            }
        }
    }
}
