using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound s_Instance { get; private set; }

    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicEffectSlider;

    static float master = 1;
    static float bgs = 1;
    static float bgm = 1;

    private void Awake()
    {
        s_Instance = this;
    }
    private void Start()
    {
        m_MusicMasterSlider.value = master;
        m_MusicBGMSlider.value = bgs;
        m_MusicEffectSlider.value = bgm;

        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicEffectSlider.onValueChanged.AddListener(SetEffectVolume);
    }
    public void SetMasterVolume(float volume)
    {
        master = volume;
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        bgs = volume;
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetEffectVolume(float volume)
    {
        bgm = volume;
        m_AudioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
    }
}
