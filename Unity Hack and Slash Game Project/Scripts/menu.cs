using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public string gameScene;
   
    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
