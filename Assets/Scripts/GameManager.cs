using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1345958/mesh-from-magicavoxel-cast-a-strange-holed-shadow.html
// magicalvoxel �� ���� ������Ʈ�� shadow bias normal ���� 0 ���� �����ϸ�
// �������� ���� �����Ǵ� �̽��� ���� �� �ִ�

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string version = "";


    public bool isInGame = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        /*var request = Resources.LoadAsync("Build", typeof(ScriptableObject));*/
        var buildObj = Resources.Load<BuildScriptableObject>("Build");
        var serverObj = Resources.Load<ServerScriptableObject>("Server");
        if (buildObj == null || serverObj == null)
        {
            Debug.LogError("Not found build or server scriptable object!");
        }
        else
        {
            // Version.{SERVER}.{PLATFORM}.{DATE}.{BUILDNUMBER} 
            instance.version = string.Format("Version.{0}.{1}.{2}.{3,0:D5}", serverObj.name.ToString(), buildObj.platform, buildObj.date, buildObj.buildNumber);
            Debug.Log(string.Format("{0}:{1}", serverObj.address.ip, serverObj.address.port));
        }

    }

    private void OnDestroy()
    {
        instance = null;
    }
    
}
