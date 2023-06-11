using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    
    void Start()
    {
        if (FinalBoss.gameComplete)
        {
            sceneName = "Credit Scene";
            FinalBoss.gameComplete = false;
        }
    }
    public void OnEnable()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
