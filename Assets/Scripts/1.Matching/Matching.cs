using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matching : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForInGame());
    }

    IEnumerator WaitForInGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("4.InGame");
    }

}
