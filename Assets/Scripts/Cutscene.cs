using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene : MonoBehaviour
{
    
    //Fields
    //Text component
    public TMP_Text dialogueText;
    //Dialogues list
    public List<string> dialogues;
    //Writing speed
    public float writingSpeed;
    //Index on dialogue
    private int index;
    //Character index
    private int charIndex;
    //Started boolean
    private bool started;
    //Wait for next boolean
    private bool waitForNext;
    public AudioSource dialogueSFX;
    public GameObject[] cutsceneImage;
    public GameObject loadScene;
    public AudioClip lastCutscene;

    bool isDialogue = true;
    void Start()
    {
        StartDialogue();
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

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
            return;

        //Boolean to indicate that we have started
        started = true;
        //Start with first dialogue
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;
        //Reset the character index
        charIndex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;
        //Start writing
        StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        //Stared is disabled
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all Ienumerators
        StopAllCoroutines();
        //Hide the window
        loadScene.SetActive(true);
    }
    //Writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        dialogueSFX.Play();
        //increase the character index 
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            //Wait x seconds 
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }

    private void Update()
    {
        if (!started)
            return;

        if (waitForNext && Input.GetButton("Fire1"))
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
