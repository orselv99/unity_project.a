using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    private Text applicationVersion = null;

    private void Start()
    {
        this.applicationVersion.text = GameManager.instance.applicationVersion;
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
    public void OnClickExit()
    {
        Application.Quit();
    }
}


