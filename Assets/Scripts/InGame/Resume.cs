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
        // 다시한번 확인
    }
    public void OnClickResume()
    {
        var canvas = GetComponentInParent<Canvas>();
        canvas.gameObject.SetActive(false);
    }

}
