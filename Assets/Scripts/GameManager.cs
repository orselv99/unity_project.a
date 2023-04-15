using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1345958/mesh-from-magicavoxel-cast-a-strange-holed-shadow.html
// magicalvoxel 로 만든 오브젝트는 shadow bias normal 값을 0 으로 조정하면
// 이음새에 빛이 투과되는 이슈를 막을 수 있다

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private bool isInitialize = false;
    
    public string applicationVersion = "";  // Lobby
    public Settings.Setting setting;
    public bool isPlayingGame = false;      // Settings

    private string GetApplicationVersion()
    {
        // 빌드정보
        var buildObj = Resources.Load<BuildScriptableObject>("Build");
        if (buildObj == null)
        {
            Debug.LogError("Not found build scriptable object!");
            Application.Quit();
        }

        // 서버정보
        var serverObj = Resources.Load<ServerScriptableObject>("Server");
        if (serverObj == null)
        {
            Debug.LogError("Not found server scriptable object!");
            Application.Quit();
        }

        Debug.Log(string.Format("{0}:{1}", serverObj.address.ip, serverObj.address.port));
        
        return string.Format("Version.{0}.{1}.{2}.{3,0:D5}", serverObj.name.ToString(), buildObj.platform, buildObj.date, buildObj.buildNumber);
    }
    private void Initialize()
    {
        if (isInitialize == true)
        {
            Debug.Log("GameManager is initialized already.");
            return;
        }

        // 화면에 표시할 버전정보
        applicationVersion = GetApplicationVersion();

        // 서버확인

        // 사용자 설정
        Settings.Initialize(ref setting);

        isInitialize = true;
    }

    private void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        instance.Initialize();

        Debug.Log("GameManager is initialized.");
    }
    
}
