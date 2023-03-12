using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterIntro : MonoBehaviour
{
    public GameObject startGame;
    public Text chapterIntro;
    public Text chapterText;
    private bool canStartGame;
    private AudioSource selection;
    public static int chapters = 1;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameManager>().health = 100;
        selection = GetComponent<AudioSource>();
        switch (chapters)
        {
            case 1:
                chapterIntro.text = "Mission 1";
                chapterText.text = "Return of the Ninja of Fortune" + "\n\n" +"-MacArthur Base, Korean Peninsula DMZ-";
                break;
            case 2:
                chapterIntro.text = "Mission 2";
                chapterText.text = "Desert Ninjas" + "\n\n" + "-Kandahar Province, Afghanistan-";
                break;
            case 3:
                chapterIntro.text = "Mission 3";
                chapterText.text = "Beginning of the End";
                break;
            case 4:
                chapterIntro.text = "Final Mission";
                chapterText.text = "The Truth";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && startGame.activeInHierarchy && !canStartGame)
        {
            selection.Play();
            canStartGame = true;
            Invoke("LoadScene", 1f);
        }
    }

    private void LoadScene()
    {
        switch (chapters)
        {
            case 1:   
                SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(5);
                break;
            case 3:
                SceneManager.LoadScene(7);
                break;
            case 4:
                SceneManager.LoadScene(9);
                break;
            default:
                break;
        }
        canStartGame = false;
    }
}
