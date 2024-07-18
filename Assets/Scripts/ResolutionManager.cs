using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        ApplyDefaultResolution();
    }

    public void ApplyDefaultResolution()
    {
        int screenWidth = ResolutionSettings.instance.defaultScreenWidth;
        int screenHeight = ResolutionSettings.instance.defaultScreenHeight;

        // 해상도 설정을 적용합니다.
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);
    }

    // 다음 씬으로 이동할 때 기본 해상도를 유지하기 위해 설정을 저장합니다.
    public void SaveResolutionSettings()
    {
        // 설정 저장이 필요 없을 경우 여기에 추가
    }
}
