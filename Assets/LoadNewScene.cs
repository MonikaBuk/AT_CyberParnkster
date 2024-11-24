using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string sceneName = "NewScene"; 
    public float delay = 3.0f;

    private void OnEnable()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay); 
        SceneManager.LoadScene(sceneName);
    }
}
