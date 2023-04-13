using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lobby : MonoBehaviour
{
    public Text versionText = null;

    private void Awake()
    {
        // 서버연결확인

        this.versionText.text = GameManager.instance.version;
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("1.Matching");
    }

    public void OnClickDashBoard()
    {
        SceneManager.LoadScene("2.DashBoard");
    }

    public void OnClickSettings()
    {
        SceneManager.LoadScene("3.Settings");
    }
}


