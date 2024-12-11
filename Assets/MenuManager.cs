using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class MenuManager : MonoBehaviour
{




    public void LoadFirstSecene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void ExitApplication()
    {
       
           
            Application.Quit();
        
    }
}
