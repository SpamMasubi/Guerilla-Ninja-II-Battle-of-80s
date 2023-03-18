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

    void Start()
    {
        switch (ChapterIntro.chapters)
        {
            case 3:
                dialogues = new List<string>() { "'John Smith': Great job, team! The Afghan Mujahideen can continue on their fight for their country. Our NINJAs can do the clean up",
                "the mess the syndicate made. Our work isn't done yet. We got another mission in the Angolan-South West African border zone.",
                "The MPLA have asked for our help. Apparently, the syndicate have given aid to their rival, UNITA. We have intel that in return",
                "for their help in the civil war, UNITA will hand over discarded nuclear weapons from South Africa to the synidcate. If they get those",
                "nuclear weapons, they will be armed with them in ready to create World War 3. We cannot allow them to possess such dangerous",
                "weapon that will end humanity. You will be in MPLA territory near the border. Our scouts have found North Star Army presence in the",
                "area. Do whatever it takes to make sure the syndicate won't make the deal for nuclear weapons with UNITA! Good luck, team!"};
                cutsceneImage[0].SetActive(false);
                cutsceneImage[1].SetActive(true);
                break;
            case 4:
                cutsceneImage[1].SetActive(false);
                cutsceneImage[2].SetActive(true);
                break;
            case 8:
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
        StartDialogue();
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
