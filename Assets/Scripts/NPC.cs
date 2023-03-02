using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public DialogueBoxClose dialogueScript;
    private bool playerDetected;

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
            dialogueScript.StartDialogue();
            playerDetected = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
