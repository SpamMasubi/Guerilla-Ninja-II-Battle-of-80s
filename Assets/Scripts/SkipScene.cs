using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public string sceneSkipName;
    AudioSource skipConfirm;
    private bool skip;

    // Start is called before the first frame update
    void Start()
    {
        if (FinalBoss.gameComplete)
        {
            sceneSkipName = "Credit Scene";
        }
        skipConfirm = GetComponent<AudioSource>();
        skip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && !skip)
        {
            skip = true;
            skipConfirm.Play();
            Invoke("loadScene", 1f);
        }
    }

    private void loadScene()
    {
        SceneManager.LoadScene(sceneSkipName, LoadSceneMode.Single);
        skip = false;
        FinalBoss.gameComplete = false;
    }
}
