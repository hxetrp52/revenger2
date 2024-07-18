using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screensetting : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        // ResolutionSettings에서 기본 해상도 정보 가져오기
        int defaultScreenWidth = ResolutionSettings.instance.defaultScreenWidth;
        int defaultScreenHeight = ResolutionSettings.instance.defaultScreenHeight;

        // 해상도 옵션을 초기화합니다.
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == defaultScreenWidth &&
                resolutions[i].height == defaultScreenHeight)
            {
                currentResolutionIndex = i;
            }
        }

        // 일반적인 해상도 비율을 추가합니다.
        AddResolutionOption(options, 16, 9);
        AddResolutionOption(options, 16, 10);
        AddResolutionOption(options, 4, 3);

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 토글 버튼 초기값 설정
        fullscreenToggle.isOn = Screen.fullScreen;

        // 이벤트 리스너 추가
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void AddResolutionOption(List<string> options, int widthRatio, int heightRatio)
    {
        foreach (Resolution res in resolutions)
        {
            if (IsRatioMatch(res, widthRatio, heightRatio))
            {
                string option = res.width + " x " + res.height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                }
            }
        }
    }

    private bool IsRatioMatch(Resolution resolution, int widthRatio, int heightRatio)
    {
        int gcd = GCD(resolution.width, resolution.height);
        int width = resolution.width / gcd;
        int height = resolution.height / gcd;

        return width == widthRatio && height == heightRatio;
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            // Settings.json 파일에 저장된 기본 해상도 선택 시
            Screen.SetResolution(ResolutionSettings.instance.defaultScreenWidth, ResolutionSettings.instance.defaultScreenHeight, Screen.fullScreen);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
