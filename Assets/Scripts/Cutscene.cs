using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public GameObject[] cutsceneImage;
    public GameObject loadScene;
    public AudioClip lastCutscene;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public AudioSource dialogueSFX;

    public float wordSpeed;
    bool isDialogue = true;
    void Start()
    {
        dialogueText.text = "";

        switch (ChapterIntro.chapters)
        {
            case 3:
                cutsceneImage[0].SetActive(false);
                cutsceneImage[1].SetActive(true);
                break;
            case 4:
                cutsceneImage[1].SetActive(false);
                cutsceneImage[2].SetActive(true);
                break;
            case 5:
                GetComponent<AudioSource>().clip = lastCutscene;
                GetComponent<AudioSource>().Play();
                cutsceneImage[2].SetActive(false);
                cutsceneImage[3].SetActive(true);
                ChapterIntro.chapters = 1;
                Destroy(FindObjectOfType<GameManager>());
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogue)
        {
            StartCoroutine(Typing());
            isDialogue = false;
        }
        else if (Input.GetButtonDown("Fire1") && dialogueText.text == dialogue[index])
        {
            NextLine();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            dialogueSFX.Play();
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            loadScene.SetActive(true); ;
        }
    }
}
