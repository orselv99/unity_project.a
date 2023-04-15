using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 동적 스크롤바 생성
// https://www.youtube.com/watch?v=X3LsvOvMpRU
public class Settings : MonoBehaviour
{
    public struct Setting
    {
        public int resolution;
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

    private struct Resolution
    {
        public int width;
        public int height;
    }
    private List<Resolution> resolutions = new List<Resolution>();

    private readonly Color H0_VALUE = new Color(144f / 255f, 61f / 255f, 98f / 255f);
    private readonly Color H1_VALUE = new Color(189f / 255f, 81f / 255f, 90f / 255f);
    private readonly Color H2_VALUE = new Color(197f / 255f, 104f / 255f, 118f / 255f);
    private readonly Color H3_VALUE = new Color(214f / 255f, 155f / 255f, 78f / 255f);
    private readonly Color H4_VALUE = new Color(243f / 255f, 207f / 255f, 64f / 255f);

    [SerializeField]
    private Dropdown resolution = null;
    [SerializeField]
    private Toggle windowMode = null;
    [SerializeField]
    private Toggle invertedAim = null;
    [SerializeField]
    private Slider[] volumeSliders = null;
    [SerializeField]
    private Image[] volumeSliderFills = null;

    // gamemanager 에서 호출됨
    // 저장된 설정이 없으면 fallback 생성
    public static void Initialize(ref Setting setting)
    {
        if (PlayerPrefs.HasKey(RESOLUTION) == false)
        {
            PlayerPrefs.SetInt(RESOLUTION, int.MaxValue);
            Debug.Log(string.Format("No key [Resolution], insert key and default value: ", int.MaxValue));
        }
        setting.resolution = PlayerPrefs.GetInt(RESOLUTION);

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
        this.resolution.ClearOptions();
        this.resolutions.Clear();

        // TODO:
        // 주사율 별 해상도 temp[i].refreshRate;
        // 수직동기화
        // [Window] 키 잠금
        var temp = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            Resolution item;
            item.width = temp[i].width;
            item.height = temp[i].height;
            this.resolutions.Add(item);
            this.resolution.options.Add(new Dropdown.OptionData(string.Format("{0} x {1}", item.width, item.height)));
        }

        this.resolution.value = (GameManager.instance.setting.resolution == int.MaxValue) ? resolutions.Count - 1 : GameManager.instance.setting.resolution;
        this.windowMode.isOn = GameManager.instance.setting.isWindowed;
        this.invertedAim.isOn = GameManager.instance.setting.isInvertedAim;
        this.volumeSliders[0].value = GameManager.instance.setting.masterVolume;
        this.volumeSliders[1].value = GameManager.instance.setting.backgroundVolume;
        this.volumeSliders[2].value = GameManager.instance.setting.effectVolume;
    }

    // ui events
    private void ChangeFillColor(int index)
    {
        var value = volumeSliders[index].value;
        if (value > 0.8f)
        {
            volumeSliderFills[index].color = H0_VALUE;
        }
        else if (value > 0.6f && value <= 0.8f)
        {
            volumeSliderFills[index].color = H1_VALUE;
        }
        else if (value > 0.4f && value <= 0.6f)
        {
            volumeSliderFills[index].color = H2_VALUE;
        }
        else if (value > 0.2f && value <= 0.4f)
        {
            volumeSliderFills[index].color = H3_VALUE;
        }
        else if (value <= 0.2f)
        {
            volumeSliderFills[index].color = H4_VALUE;
        }
    }
    public void OnValueChangedResolution(Dropdown dropdown)
    {
        var value = dropdown.value;
        GameManager.instance.setting.resolution = value;
        PlayerPrefs.SetInt(RESOLUTION, value);
        Screen.SetResolution(this.resolutions[value].width, this.resolutions[value].height, !GameManager.instance.setting.isWindowed);
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
        ChangeFillColor(0);
    }
    public void OnValueChangedBackgroundVolume(Slider slider)
    {
        var value = slider.value;
        GameManager.instance.setting.backgroundVolume = value;
        VolumeManager.instance.SetBackgroundVolume(value);
        PlayerPrefs.SetFloat(BACKGROUND_VOLUME, value);
        ChangeFillColor(1);
    }
    public void OnValueChangedEffectVolume(Slider slider)
    {
        var value = slider.value;
        GameManager.instance.setting.effectVolume = value;
        VolumeManager.instance.SetEffectVolume(value);
        PlayerPrefs.SetFloat(EFFECT_VOLUME, value);
        ChangeFillColor(2);
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
