using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 동적 스크롤바 생성
// https://www.youtube.com/watch?v=X3LsvOvMpRU
public class Settings : MonoBehaviour
{
    // display ui
    private Dropdown resolution = null;
    private Toggle windowMode = null;
    private Slider[] volumeSliders = null;

    private void Init()
    {
        // setting fallback
        if (PlayerPrefs.HasKey("Resolution") == false)
        {
            PlayerPrefs.SetString("Resolution", "1920 x 1080");
        }

        if (PlayerPrefs.HasKey("WindowMode") == false)
        {
            PlayerPrefs.SetInt("WindowMode", 0);
        }

        if (PlayerPrefs.HasKey("MasterVolume") == false)
        {
            PlayerPrefs.SetFloat("MasterVolume", 1f);
        }

        if (PlayerPrefs.HasKey("BackgroundVolume") == false)
        {
            PlayerPrefs.SetFloat("BackgroundVolume", 1f);
        }

        if (PlayerPrefs.HasKey("EffectVolume") == false)
        {
            PlayerPrefs.SetFloat("EffectVolume", 1f);
        }

        // setting 1 : 해상도
        this.resolution = GetComponentInChildren<Dropdown>();

        var resolutions = new List<string>();
        foreach (var res in Screen.resolutions)
        {
            resolutions.Add(string.Format("{0} x {1}", res.width, res.height));
        }
        resolutions.Reverse();

        this.resolution.AddOptions(resolutions);
        /*this.resolution.value = PlayerPrefs.GetString("Resolution");*/


        // setting 2 : 창모드
        this.windowMode = GetComponentInChildren<Toggle>();
        this.windowMode.isOn = (PlayerPrefs.GetInt("WindowMode") == 1 ? true : false);


        // setting 3 : 볼륨 (0: master, 1: background, 2: effect)
        this.volumeSliders = GetComponentsInChildren<Slider>();
        this.volumeSliders[0].value = PlayerPrefs.GetFloat("MasterVolume");
        this.volumeSliders[1].value = PlayerPrefs.GetFloat("BackgroundVolume");
        this.volumeSliders[2].value = PlayerPrefs.GetFloat("EffectVolume");
    }

    private void Start()
    {
        Init();
    }

    /* ui event */

    public void OnValueChangedResolution(Dropdown dropdown)
    {
        Debug.Log(string.Format("Index: {0} {1}", dropdown.value, dropdown.options[dropdown.value].text));
    }

    public void OnValueChangedWindowMode(Toggle toggle)
    {
        Screen.fullScreenMode = (toggle.isOn == true) ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
    }

    public void OnValueChangedMasterVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
    }

    public void OnValueChangedBackgroundVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("BackgroundVolume", slider.value);
    }

    public void OnValueChangedEffectVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("EffectVolume", slider.value);
    }

    public void OnClickBack()
    {
        if (GameManager.instance.isInGame == true)
        {
            SceneManager.LoadScene("4.InGame");
        }
        else
        {
            SceneManager.LoadScene("0.Lobby");
        }
    }
}
