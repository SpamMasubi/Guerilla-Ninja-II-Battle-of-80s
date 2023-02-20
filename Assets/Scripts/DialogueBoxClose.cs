using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxClose : MonoBehaviour
{
   public void startGameAfterDialogue()
    {
        NPC.isDialogue = false;
        FindObjectOfType<NPC>().RemoveText();
    }
}
