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
    public AudioClip lastCutscene, finalMissionCutscene;

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
                dialogues = new List<string>() { "'John Smith': Once again, another victory for N.I.N.J.A! It has seem that UNITA has not agree with giving away the nuclear weapon",
                "to the syndicate. The MPLA can fight UNITA without their help. Though our task in Africa is done, we have another by the United Nation.",
                "A militant group known as the United Liberation Army have taken hostages of Japanese officials in West Germany during the",
                "G7 Summit. We learned that the ULA is part of the Japanese Red Army, but their only goal is to create violent revolution and eliminate the",
                "current Japanese government. The ULA have full support from the syndicate. We must rescue the Japanese officials before they",
                "kill them. You will sneak into the Reichstag. I'll have Yumi decrypt the ULA's conversation. Remember! We cannot let any of the Japanese",
                "officials be harmed. Good luck, team!"};
                cutsceneImage[1].SetActive(false);
                cutsceneImage[2].SetActive(true);
                break;
            case 5:
                dialogues = new List<string>() { "'John Smith': Haha, nice job team. The G7 Leaders can continue to bring world peace with their meetings. We are still not done with",
                "our works yet. The US is getting ready for their presidential election. It seems that one of the candidate will put an end to this Cold",
                "War. The syndicate isn't to happy with this and we have intels that they are about to sabotage the presidential election. They want",
                "to put one of their's as another candidate for the opposing side. We can't allow them to destroy the elections! If the syndicate",
                "tampers the election and they win, this Cold War will turn into worse and the syndicate will achieve their goal for world domination. Get to",
                "it, team! Good luck, all!"};
                cutsceneImage[2].SetActive(false);
                cutsceneImage[3].SetActive(true);
                break;
            case 6:
                dialogues = new List<string>() { "'John Smith': Another job well done, team. The US can have a president that can bring world peace and end this Cold War. Still, we are not",
                "done with our work. There is another problem in Latin America. A civil war in Nicaragua has broken out amidst the Cold War. Contra",
                "forces are fighting against the communist Sandinistas for power of their countries. Our scouts have infiltrated the Contra's camp.",
                "Not only is the Contra backed by the US and the West, but the syndicate is helping to oust the Sandinistas our of the game for power of",
                "Nicaragua. We must stop them at all cost. You will assist the Sandinistas as necesary in the jungles. The North Star Army must not win",
                "this fight. Good luck, my friends."};
                cutsceneImage[3].SetActive(false);
                cutsceneImage[4].SetActive(true);
                break;
            case 7:
                GetComponent<AudioSource>().clip = finalMissionCutscene;
                GetComponent<AudioSource>().Play();
                dialogues = new List<string>() { "'John Smith': Team, we have urgent issue in our hand. Our ninjas reported Yumi has been kidnapped by North Star Army disguised as",
                "civilians while she was on her days off in Japan. The syndicate have her as their prisoner in exchange for prisoners we",
                "captured along with a huge ransom. They said if we don't comply, they will kill Yumi. Hung, I know this is a difficult situation seeing your",
                "fiance in danger. We will get her back. I promise you, my friend. However, we must carefully execute a plan that not only to save",
                "Yumi, but to eradicate the syndicate forever. We must not let Yumi be harmed. I have already sent some of the NINJA units to their home",
                "base in the South Pacific. Hokuto Island is where she is held at according to our scouts. You and the team will rendevous with our units",
                "on the island. Once Yumi is rescued, our units will launch an assault on the North Star Army and allow the UN Forces to clean up the rest.",
                "This is it! The final battle against the North Star of Heaven! We shall not let them achieve the goal of creating WWIII from this Cold War!",
                "Now, off you go! Good luck, N.I.N.J.As!"};
                cutsceneImage[4].SetActive(false);
                cutsceneImage[5].SetActive(true);
                break;
            case 8:
                GetComponent<AudioSource>().clip = lastCutscene;
                GetComponent<AudioSource>().Play();
                dialogues = new List<string>() { "'John Smith': Excellent job, team. The UN Task Force has just arrived on time to clean up the mess. The North Star of Heaven syndicate is",
                "no more. Though the Cold War still rages on, we can assure that it will not lead to another World War. I have sent some of our NINJAs to",
                "help with the UN Task Force to dismantle any nukes or weapons that was going to be used by the syndicate to start the war. Once again,",
                "the world is at peace for the time being. We have rescued Yumi and our NINJAs back from the grasp the syndicate. However, I fear that ",
                "a new threat will rise from the legacy left by the syndicate. Fortunate enough, we will still take them head on when the time comes. For ",
                "now, you all deserve a nice vacation after this. I have provided all necessities needed for your vacation. My treat to you all. Until",
                "next time, my friends. Be at the ready for the unexpected."};
                cutsceneImage[5].SetActive(false);
                cutsceneImage[6].SetActive(true);
                ChapterIntro.chapters = 1;
                Destroy(FindObjectOfType<GameManager>());
                break;
            default:
                cutsceneImage[6].SetActive(false);
                cutsceneImage[0].SetActive(true);
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
