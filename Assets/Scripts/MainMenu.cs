using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource playGame, titleSong;
    public AudioClip selectionHighlight, selected;
    private bool hasStarted;
    public static int characterNum = 0;
    public AudioClip[] ninjaStartVoice;
    public GameObject[] ninjas;

    public Text highScoreText, yourScoreText;

    public GameObject characterSelection, mainMenu, HighScoreMenu, DeleteDataOption, WebGLCharacterSelection, DeleteDataText;
    public GameObject WebGLMenu, StandAloneMenu; //StandAlone and WebGLMenu

    public GameObject firstCharacterButton, characterSelectionMenuButton, HighScoreCloseButton, closeOption;
    public GameObject WebGLCharacterSelectionBackButton, WebGLFirstCharacterButton, WebGLHighScoreCloseButton, WebGLCloseOption; //WebGL

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

    private void Update()
    {
        if (highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetInt("High Score").ToString();
        }
        if (yourScoreText != null)
        {
            yourScoreText.text = PlayerPrefs.GetInt("Your Score").ToString();
        }
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
        GetComponent<AudioSource>().PlayOneShot(selected);
        Application.Quit();
    }

    void LoadScene()
    {
        hasStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToStageSelection()
    {
        if (!hasStarted)
        {
            GetComponent<AudioSource>().PlayOneShot(selected);
            hasStarted = true;
            Invoke("LoadtoStageSelection", 3f);
        }
    }

    void LoadtoStageSelection()
    {
        hasStarted = false;
        SceneManager.LoadScene("Selection Stage");
    }

    public void characterSelectionOpen()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(true);
        WebGLMenu.SetActive(false);
        StandAloneMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLCharacterSelectionBackButton));
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
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(false);
        WebGLMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLFirstCharacterButton);
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

    public void HighScoreOpen()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(true);
        WebGLMenu.SetActive(false);
        StandAloneMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLHighScoreCloseButton);
#endif

#if UNITY_STANDALONE
        HighScoreMenu.SetActive(true);
        mainMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(HighScoreCloseButton);
#endif

    }

    public void HighScoreClose()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLStoryMenu.SetActive(false);
        WebGLMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(WebGLFirstCharacterButton);
#endif

#if UNITY_STANDALONE
        HighScoreMenu.SetActive(false);
        mainMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(characterSelectionMenuButton);
#endif
    }

    public void OpenDeleteDataOption()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLDeleteDataOption.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(closeOption);
#endif
        DeleteDataOption.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(closeOption);
    }    
    
    public void CloseDeleteDataOption()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
#if UNITY_WEBGL
        WebGLDeleteDataOption.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(closeOption);
#endif
        DeleteDataOption.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(HighScoreCloseButton);
    }

    public void DataDeleted()
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
        DeleteDataText.SetActive(true);
        DeleteDataOption.SetActive(false);
        PlayerPrefs.DeleteAll();
        StartCoroutine(ReturnToHSMenu());
    }

    IEnumerator ReturnToHSMenu()
    {
        yield return new WaitForSeconds(3f);
        DeleteDataText.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(HighScoreCloseButton);
    }

    public void highLightSound()
    {
        GetComponent<AudioSource>().PlayOneShot(selectionHighlight);
    }
}
