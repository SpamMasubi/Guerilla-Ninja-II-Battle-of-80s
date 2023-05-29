using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBoxClose : MonoBehaviour
{

    //Fields
    //Window
    public GameObject window;
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
    public static bool started;
    //Wait for next boolean
    private bool waitForNext;

    public Image im;
    public Sprite finalBossImage;

    AudioSource dialogueSFX;
    public Animator DialogueAnimation;

    public void Start()
    {
        dialogueSFX = GetComponent<AudioSource>();
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
            return;

        //Boolean to indicate that we have started
        started = true;
        //Show the window
        window.SetActive(true);
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
        window.SetActive(false);
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
                if (FindObjectOfType<FinalBoss>().gameObject)
                {
                    BossStart.startBoss = true;
                }
                DialogueAnimation.SetTrigger("CloseDialogue");
            }
        }
    }

    public void FinalBossDialogue()
    {
        im.sprite = finalBossImage;
        dialogues = new List<string>() { "Son Ngo Khong: Well well. It seems you have defeated my men to make your way through this island to rescue your precious friends. I",
                "will show you that on Hokuto Island, no one leaves here alive. The North Star of Heaven shows no mercy to any enemies we encounter.",
                "Our ultimate goal of boosting a Third World War from this 'Cold War' shall come to fruition! No longer will the Western World and Eastern",
                "Bloc will fight amongst each other. Instead, we shall help them end it with their own destructions. Soon, the world shall bow down to us",
                "to us. But...before that can happen...you N.I.N.J.As are in the way of our plan. I will make sure your death will be slow and",
                "painless as I will make your friends here watch you die! I have always acquired the arts of Ninjutsu as well. Taste the power of the",
                "North Star Ninjutsu! YOU'RE FINISH!!!!"};
    }
}
