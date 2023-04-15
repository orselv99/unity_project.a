using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 동적 스크롤바 생성
// https://www.youtube.com/watch?v=X3LsvOvMpRU
public class Settings : MonoBehaviour
{
    public struct Setting
    {
        public string resolution;
        public bool isWindowed;
        public bool isInvertedAim;
        public float masterVolume;
        public float backgroundVolume;
        public float effectVolume;
    }

    private static string RESOLUTION = "Resolution";
    private static string WINDOW_MODE = "WindowMode";
    private static string INVERTED_AIM = "InvertedAim";
    private static string MASTER_VOLUME = "MasterVolume";
    private static string BACKGROUND_VOLUME = "BackgroundVolume";
    private static string EFFECT_VOLUME = "EffectVolume";

    [SerializeField]
    private Dropdown resolution = null;
    [SerializeField]
    private Toggle windowMode = null;
    [SerializeField]
    private Toggle invertedAim = null;
    [SerializeField]
    private Slider[] volumeSliders = null;

    // gamemanager 에서 호출됨
    // 저장된 설정이 없으면 fallback 생성
    public static void Initialize(ref Setting setting)
    {
        if (PlayerPrefs.HasKey(RESOLUTION) == false)
        {
            PlayerPrefs.SetString(RESOLUTION, "1920 x 1080");
            Debug.Log("No key [Resolution], insert key and default value: 1920 x 1080");
        }
        setting.resolution = PlayerPrefs.GetString(RESOLUTION);

        if (PlayerPrefs.HasKey(WINDOW_MODE) == false)
        {
            PlayerPrefs.SetInt(WINDOW_MODE, 0);
            Debug.Log("No key [WindowMode], insert key and default value: 0");
        }
        setting.isWindowed = (PlayerPrefs.GetInt(WINDOW_MODE) == 1) ? true : false;

        if (PlayerPrefs.HasKey(INVERTED_AIM) == false)
        {
            PlayerPrefs.SetInt(INVERTED_AIM, 0);
            Debug.Log("No key [InvertedAim], insert key and default value: 0");
        }
        setting.isInvertedAim = (PlayerPrefs.GetInt(INVERTED_AIM) == 1) ? true : false;

        if (PlayerPrefs.HasKey(MASTER_VOLUME) == false)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME, 1f);
            Debug.Log("No key [MasterVolume], insert key and default value: 1f");
        }
        setting.masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME);

        if (PlayerPrefs.HasKey(BACKGROUND_VOLUME) == false)
        {
            PlayerPrefs.SetFloat(BACKGROUND_VOLUME, 1f);
            Debug.Log("No key [BackgroundVolume], insert key and default value: 1f");
        }
        setting.backgroundVolume = PlayerPrefs.GetFloat(BACKGROUND_VOLUME);

        if (PlayerPrefs.HasKey(EFFECT_VOLUME) == false)
        {
            PlayerPrefs.SetFloat(EFFECT_VOLUME, 1f);
            Debug.Log("No key [EffectVolume], insert key and default value: 1f");
        }
        setting.effectVolume = PlayerPrefs.GetFloat(EFFECT_VOLUME);
    }

    private void Start()
    {
        var resolutions = new List<string>();
        foreach (var res in Screen.resolutions)
        {
            resolutions.Add(string.Format("{0} x {1}", res.width, res.height));
        }
        resolutions.Reverse();
        this.resolution.AddOptions(resolutions);

        /*this.resolution.value = PlayerPrefs.GetString("Resolution");*/
        this.windowMode.isOn = GameManager.instance.setting.isWindowed;
        this.invertedAim.isOn = GameManager.instance.setting.isInvertedAim;
        this.volumeSliders[0].value = GameManager.instance.setting.masterVolume;
        this.volumeSliders[1].value = GameManager.instance.setting.backgroundVolume;
        this.volumeSliders[2].value = GameManager.instance.setting.effectVolume;
    }

    // ui events
    public void OnValueChangedResolution(Dropdown dropdown)
    {
        Debug.Log(string.Format("Index: {0} {1}", dropdown.value, dropdown.options[dropdown.value].text));
    }
    public void OnValueChangedWindowMode(Toggle toggle)
    {
        var isChecked = toggle.isOn;
        GameManager.instance.setting.isWindowed = isChecked;
        PlayerPrefs.SetInt(WINDOW_MODE, (isChecked == true) ? 1 : 0);
        Screen.fullScreenMode = (isChecked == true) ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
    }
    public void OnValueChangedInvertedAim(Toggle toggle)
    {
        var isChecked = toggle.isOn;
        GameManager.instance.setting.isInvertedAim = isChecked;
        PlayerPrefs.SetInt(INVERTED_AIM, (isChecked == true) ? 1 : 0);
    }
    public void OnValueChangedMasterVolume(Slider slider)
    {
        var value = slider.value;
        GameManager.instance.setting.masterVolume = value;
        VolumeManager.instance.SetMasterVolume(value);
        PlayerPrefs.SetFloat(MASTER_VOLUME, value);
    }
    public void OnValueChangedBackgroundVolume(Slider slider)
    {
        var value = slider.value;
        GameManager.instance.setting.backgroundVolume = value;
        VolumeManager.instance.SetBackgroundVolume(value);
        PlayerPrefs.SetFloat(BACKGROUND_VOLUME, value);
    }
    public void OnValueChangedEffectVolume(Slider slider)
    {
        var value = slider.value;
        GameManager.instance.setting.effectVolume = value;
        VolumeManager.instance.SetEffectVolume(value);
        PlayerPrefs.SetFloat(EFFECT_VOLUME, value);
    }
    public void OnClickBack()
    {
        if (GameManager.instance.isPlayingGame == true)
        {
            SceneManager.LoadScene("4.InGame");
        }
        else
        {
            SceneManager.LoadScene("0.Lobby");
        }
    }
}
