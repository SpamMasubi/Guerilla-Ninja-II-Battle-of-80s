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
    private static int chapterIncr = 5;
    public static int chapters = 6;

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
                chapterText.text = "A Weapon to Surpass" + "\n\n" + "-MPLA Zone, Angola-S.W. Africa Border-";
                break;
            case 4:
                chapterIntro.text = "Mission 4";
                chapterText.text = "Wounds That Never Heal" + "\n\n" + "-Reichstag, West Germany-";
                break;
            case 5:
                chapterIntro.text = "Mission 5";
                chapterText.text = "A Better Future" + "\n\n" + "-Long Beach (California), USA-";
                break;
            case 6:
                chapterIntro.text = "Mission 6";
                chapterText.text = "The Unclassified" + "\n\n" + "-Nicaraguan Jungle, Nicaragua-";
                break;
            case 7:
                chapterIntro.text = "Final Mission";
                chapterText.text = "The North Star of Heaven" + "\n\n" + "Hokuto Island, South Pacific";
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
        if(chapters <= 7)
        {
            SceneManager.LoadScene(chapters + 2 + chapterIncr);
            chapterIncr += 1;
        }
        else
        {
            chapterIncr = 0;
        }
        canStartGame = false;
    }
}
