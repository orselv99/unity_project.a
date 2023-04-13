using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 동적 스크롤바 생성
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
        // 게임에서 setting 창을 열었을 경우 disable

        // 로비에서 변경된 값의 유무를 check 하여, 클릭시 메시지 창 띄우기

        SceneManager.LoadScene("0.Lobby");
    }
}
