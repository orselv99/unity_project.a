using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1345958/mesh-from-magicavoxel-cast-a-strange-holed-shadow.html
// magicalvoxel �� ���� ������Ʈ�� shadow bias normal ���� 0 ���� �����ϸ�
// �������� ���� �����Ǵ� �̽��� ���� �� �ִ�

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private bool isInitialize = false;
    
    public string applicationVersion = "";  // Lobby
    public Settings.Setting setting;
    public bool isPlayingGame = false;      // Settings

    private string GetApplicationVersion()
    {
        // ��������
        var buildObj = Resources.Load<BuildScriptableObject>("Build");
        if (buildObj == null)
        {
            Debug.LogError("Not found build scriptable object!");
            Application.Quit();
        }

        // ��������
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

        // ȭ�鿡 ǥ���� ��������
        applicationVersion = GetApplicationVersion();

        // ����Ȯ��

        // ����� ����
        Settings.Initialize(ref setting);

        isInitialize = true;
    }

    private void Awake()
    {
        // �̱���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        instance.Initialize();

        Debug.Log("GameManager is initialized.");
    }
    
}
