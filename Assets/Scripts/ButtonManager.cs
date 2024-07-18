using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;
//using static System.Net.Mime.MediaTypeNames;
//using Image = UnityEngine.UI.Image;

public class ButtonManager : MonoBehaviour
{ 
    public GameObject optionpanel; // 옵션창 가져오기
    public AudioSource effectsource;  // 버튼 효과음 실행을 위한 소스

    public GameObject sound_panel;
    public GameObject screen_panel;
    public GameObject control_panel;

    public Sprite mute_img; // 뮤트 이미지
    public Sprite unmute_img; // 음표 이미지

    public Image sound_img1;// 마스터 볼륨 이미지
    public Image sound_img2;// 이펙트 볼룸 이미지
    public Image sound_img3;// 배경 볼륨 이미지

    public Slider sound_Slider1;// 마스터 볼륨 슬라이드
    public Slider sound_Slider2;// 이펙트 볼륨 슬라이드
    public Slider sound_Slider3;// 배경 볼륨 슬라이드

    public Image sound_button_img;
    public Image screen_button_img;
    public Image control_button_img;

    private bool optionpanel_on;
    private bool sound_button = true;
    private bool screen_button = false;
    private bool control_button = false;

    //현재 뮤트가 되었는지 저장하는 변수
    private bool isMuteMasterVolum;
    private bool isMuteBGSVolum;
    private bool isMuteBGMVolum;

    //뮤트 시 이전 볼륨 저장
    public float masterVolum;
    public float bgsVolum;
    public float bgmVolum;

    void Start()
    {
        OnOffPannel(false);// 시작시 옵션 패널 꺼짐
        effectsource = GetComponent<AudioSource>();
    }
    private void OnOffPannel(bool active)
    {
        optionpanel.SetActive(active);
        optionpanel_on = active;
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && optionpanel_on == true) // esc를 눌렀을 때 옵션 창 닫기 
        {
            effectsource.Play();
            OnOffPannel(false);
        }

        if (sound_Slider1.value == 0.001f) // 미안하다
        {
            sound_img1.sprite = mute_img;
        }
        else
        {
            sound_img1.sprite = unmute_img;
        }

        if (sound_Slider2.value == 0.001f) // 일단 지금은 구현만함
        {
            sound_img2.sprite = mute_img;
        }
        else
        {
            sound_img2.sprite = unmute_img;
        }

        if (sound_Slider3.value == 0.001f) // 나중에 최적화할게 
        {
            sound_img3.sprite = mute_img;
        }
        else
        {
            sound_img3.sprite = unmute_img;
        }

        if( sound_button == true )
        {
            
            sound_panel.SetActive(true);
            sound_button_img.color = new Color(45 / 255f, 45 / 255f, 45 / 255f, 255 / 255f);
        }
        else
        {
            sound_panel.SetActive(false);
            sound_button_img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }

        if (screen_button == true)
        {
            
            screen_panel.SetActive(true);
            screen_button_img.color = new Color(45 / 255f, 45 / 255f, 45 / 255f, 255 / 255f);
        }
        else
        {
            screen_panel.SetActive(false);
            screen_button_img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }

        if (control_button == true)
        {
            
            control_panel.SetActive(true);
            control_button_img.color = new Color(45 / 255f, 45 / 255f, 45 / 255f, 255 / 255f);
        }
        else
        {
            control_panel.SetActive(false);
            control_button_img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void StartGame()// 게임씬으로 넘어가는 함수 
    {
        effectsource.Play();
        SceneManager.LoadScene(1);
    }
    public void OptionpPanel()// 옵션창 열기 함수
    {
        effectsource.Play();
        OnOffPannel(true); 

    }
    public void OptionpPanel_Close()
    {
        effectsource.Play();
        OnOffPannel(false);
    }
    public void ExitGame() // 게임 나가기 함수
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    
    public void mute_MV() // 마스터 볼륨 음소거
    {
        effectsource.Play();
        if (isMuteMasterVolum)
        {
            sound_Slider1.value = masterVolum;
        }
        else
        {
            masterVolum = sound_Slider1.value;
            sound_Slider1.value = 0.001f;
        }
        isMuteMasterVolum = !isMuteMasterVolum;
    }
    public void mute_EV() // 이펙트 볼륨 음소거
    {
        effectsource.Play();
        if (isMuteBGSVolum)
        {
            sound_Slider2.value = bgsVolum;
        }
        else
        {
            bgsVolum = sound_Slider2.value;
            sound_Slider2.value = 0.001f;
        }
        isMuteBGSVolum = !isMuteBGSVolum;
    }
    public void mute_BV() // 배경음악 음소거
    {
        effectsource.Play();
        if (isMuteBGMVolum)
        {
            sound_Slider3.value = bgmVolum;
        }
        else
        {
            bgmVolum = sound_Slider3.value;
            sound_Slider3.value = 0.001f;
        }
        isMuteBGMVolum = !isMuteBGMVolum;
    }

    public void open_soundpanel()
    {
        sound_button = true;
        screen_button = false;
        control_button = false;
    }

    public void open_screenpanel()
    {
        sound_button = false;
        screen_button = true;
        control_button = false;
    }

    public void open_controlpanel()
    {
        sound_button = false;
        screen_button = false ;
        control_button = true;
    }
}
