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
        var resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {

        }
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
