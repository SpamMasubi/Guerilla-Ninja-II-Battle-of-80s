using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StageSelection : MonoBehaviour
{
    public AudioClip selectionHighlight, selected;
    public AudioClip[] charactersSFX;
    public GameObject[] ninjas;
    public GameObject characterSelection;
    public GameObject firstCharacterButton, firstStageButton;
    private int stage;

    [Header("Dialgoue")]
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
    public Sprite finalStageImage, originalStageImage;

    public Animator DialogueAnimation;

    private bool hasStarted;
    public static bool replayMode;

    // Start is called before the first frame update
    void Start()
    {
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstStageButton);
        FindObjectOfType<GameManager>().ResetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        if (waitForNext)
        {
            waitForNext = false;
            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
        }
    }

    public void PlayGame(string ninjaCharacterName)
    {
        if (!hasStarted)
        {
            switch (ninjaCharacterName)
            {
                case "Guerilla Ninja":
                    MainMenu.characterNum = 1;
                    GetComponent<AudioSource>().PlayOneShot(charactersSFX[0]);
                    break;
                case "Shinobi":
                    MainMenu.characterNum = 2;
                    GetComponent<AudioSource>().PlayOneShot(charactersSFX[1]);
                    break;
                case "Kunoichi":
                    MainMenu.characterNum = 3;
                    GetComponent<AudioSource>().PlayOneShot(charactersSFX[2]);
                    break;
                default:
                    break;
            }
            hasStarted = true;
            Invoke("LoadScene", 3f);
        }
    }

    public void characterSelectionOpen(int stageNumber)
    {
        GetComponent<AudioSource>().PlayOneShot(selected);
        stage = stageNumber;
        CloseDialogue();
        characterSelection.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstCharacterButton);
    }

    public void characterSelectionClose()
    {
        stage = 0;
        characterSelection.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstStageButton);
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
        GetComponent<AudioSource>().PlayOneShot(selectionHighlight);
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

    public void CloseDialogue()
    {
        DialogueAnimation.SetTrigger("CloseDialogue");
        EndDialogue();
    }

    void LoadScene()
    {
        hasStarted = false;
        SceneManager.LoadScene("Level " + stage + "-1");
    }

    public void BackToMainMenu()
    {
        if (!hasStarted)
        {
            GetComponent<AudioSource>().PlayOneShot(selected);
            hasStarted = true;
            Invoke("LoadBackToMainMenu", 3f);
        }
    }

    void LoadBackToMainMenu()
    {
        hasStarted = false;
        replayMode = false;
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Title");
    }

    public void highLightSound()
    {
        GetComponent<AudioSource>().PlayOneShot(selectionHighlight);
    }

    public void startStageDialogue(int stageDialogue)
    {
        DialogueAnimation.Rebind();
        if(stageDialogue == 7)
        {
            im.sprite = finalStageImage;
        }
        else
        {
            im.sprite = originalStageImage;
        }
        switch (stageDialogue)
        {
            case 2:
                dialogues = new List<string>() {"Yumi: During the Soviet-Afghan War, the mujahideen is expecting a delivering of a new Stinger missile. However, it was stolen."};
                break;
            case 3:
                dialogues = new List<string>() { "Yumi: UNITA has been promised nukes if the North Star of Heaven helps them win against the MPLA in the Angolan Civil War."};
                break;
            case 4:
                dialogues = new List<string>() { "Yumi: Japanese communist guerillas have taken hostage of the G7 leaders in West Germany for their revolutionary bloodshed."};
                break;
            case 5:
                dialogues = new List<string>() { "Yumi: The syndicate is planning to sabatoge the US presidential election by putting one of their own to start their goals of WWIII sooner."};
                break;
            case 6:
                dialogues = new List<string>() { "Yumi: A civil war in Nicaragua has caught attention of the syndicate that they are providing aid to the Western-backed Contras."};
                break;
            case 7:
                dialogues = new List<string>() { "'John Smith': This is the finale. Our members, including Yumi, have been captured by the syndicate. We must rescue them at all cost." };
                break;
            case 8:
                dialogues = new List<string>() { "Yumi: Remenants of the North Star Army have gone rouge and taken advantage of post-war Cambodia for the next in power." };
                break;
            default:
                dialogues = new List<string>() {"Yumi: The North Star are planning to reignite the war in the Korean Penisula. Many innocent on both side will die if the war starts again."};
                break;
        }
        //Stared is disabled
        started = false;
        //Disable wait for next as well
        waitForNext = false;
        //Stop all Ienumerators
        StopAllCoroutines();
        StartDialogue();
    }
}
