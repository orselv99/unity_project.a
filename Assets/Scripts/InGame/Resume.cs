using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    public void OnClickSettings()
    {
        SceneManager.LoadScene("3.Settings");
    }
    public void OnClickLeave()
    {
        // �ٽ��ѹ� Ȯ��
    }
    public void OnClickResume()
    {
        var canvas = GetComponentInParent<Canvas>();
        canvas.gameObject.SetActive(false);
    }

}
