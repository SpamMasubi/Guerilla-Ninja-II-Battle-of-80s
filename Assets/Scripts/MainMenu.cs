using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public AudioSource playGame, titleSong;
    private bool hasStarted;
    public static int characterNum = 0;
    public AudioClip[] ninjaStartVoice;
    public GameObject[] ninjas;
    public GameObject characterSelection, mainMenu, WebGLCharacterSelection;
    public GameObject WebGLMenu, StandAloneMenu; //StandAlone and WebGLMenu

    public GameObject startGameButton, firstCharacterButton, characterSelectionMenuButton;
    public GameObject WebGLstartGameButton, WebGLCharacterSelectionBackButton, WebGLstoryCloseButton; //WebGL

    void Start()
    {
#if UNITY_WEBGL
        WebGLMenu.SetActive(true);
        StandAloneMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLstartGameButton);

#endif

#if UNITY_STANDALONE

        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(characterSelectionMenuButton);

#endif

    }

    public void PlayGame(string ninjaCharacterName)
    {
        characterSelection.SetActive(false);
        if (!hasStarted)
        {
            switch (ninjaCharacterName)
            {
                case "Guerilla Ninja":
                    characterNum = 1;
                    ninjas[0].GetComponent<Animator>().SetTrigger("Start Game");
                    GetComponent<AudioSource>().PlayOneShot(ninjaStartVoice[0]);
                    break;
                case "Shinobi":
                    characterNum = 2;
                    ninjas[1].GetComponent<Animator>().SetTrigger("Start Game");
                    GetComponent<AudioSource>().PlayOneShot(ninjaStartVoice[1]);
                    break;
                case "Kunoichi":
                    characterNum = 3;
                    ninjas[2].GetComponent<Animator>().SetTrigger("Start Game");
                    GetComponent<AudioSource>().PlayOneShot(ninjaStartVoice[2]);
                    break;
                default:
                    break;
            }
            hasStarted = true;
            playGame.Play();
            titleSong.Stop();
            Invoke("LoadScene", 7.5f);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadScene()
    {
        hasStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void characterSelectionOpen()
    {
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(true);
        WebGLMenu.SetActive(false);
        StandAloneMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLstoryBackButton);
#endif

#if UNITY_STANDALONE
        characterSelection.SetActive(true);
        mainMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstCharacterButton);
#endif

    }

    public void characterSelectionClose()
    {
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(false);
        WebGLMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLstoryCloseButton);
#endif

#if UNITY_STANDALONE
        characterSelection.SetActive(false);
        mainMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(characterSelectionMenuButton);
#endif
    }
}
