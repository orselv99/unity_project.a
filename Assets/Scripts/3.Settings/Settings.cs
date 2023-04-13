using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ��ũ�ѹ� ����
// https://www.youtube.com/watch?v=X3LsvOvMpRU
public class Settings : MonoBehaviour
{
    public struct Setting
    {
        public Text name;
        public Selectable ui;
    }
    public Setting[] settings = null;

    private void Awake()
    {
        // setting 1 : �ػ�
        var resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            Debug.Log(string.Format("{0} x {1}", resolution.width, resolution.height));
        }
        settings[0].name.text = "RESOLUTION";
        settings[0].ui = gameObject.AddComponent<Dropdown>();

        Screen.SetResolution(1920, 1080, true);

        // 
    }

    public void OnClickSave()
    {
        SceneManager.LoadScene("0.Lobby");
    }

    public void OnClickAbort()
    {
        // ���ӿ��� setting â�� ������ ��� disable

        // �κ񿡼� ����� ���� ������ check �Ͽ�, Ŭ���� �޽��� â ����

        SceneManager.LoadScene("0.Lobby");
    }
}
