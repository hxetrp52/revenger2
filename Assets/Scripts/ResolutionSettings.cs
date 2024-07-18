using UnityEngine;
using System.IO;

public class ResolutionSettings : MonoBehaviour
{
    public static ResolutionSettings instance;

    public int defaultScreenWidth = 1920;
    public int defaultScreenHeight = 1080;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadSettings()
    {
        string filePath = Application.dataPath + "/Settings.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ResolutionData data = JsonUtility.FromJson<ResolutionData>(json);
            defaultScreenWidth = data.defaultScreenWidth;
            defaultScreenHeight = data.defaultScreenHeight;
        }
    }
}

[System.Serializable]
public class ResolutionData
{
    public int defaultScreenWidth;
    public int defaultScreenHeight;
}
