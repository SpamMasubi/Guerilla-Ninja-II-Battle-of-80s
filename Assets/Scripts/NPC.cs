using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public DialogueBoxClose dialogueScript;
    private bool playerDetected;

    void Start()
    {
        if (FindObjectOfType<DialogueBoxClose>() != null) 
        {
            dialogueScript = FindObjectOfType<DialogueBoxClose>();
        }
    }
    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we triggerd the player enable playerdeteced and show indicator
        if (collision.tag == "Player")
        {
            playerDetected = true;
        }
    }
    //While detected if we interact start the dialogue
    private void Update()
    {
        if (playerDetected)
        {
            if (FindObjectOfType<FinalBoss>())
            {
                dialogueScript.FinalBossDialogue();
            }
            dialogueScript.StartDialogue();
            playerDetected = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
