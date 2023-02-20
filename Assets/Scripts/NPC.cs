using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public Animator DialogueAnimation;
    public AudioSource dialogueSFX;

    public float wordSpeed;
    public static bool isDialogue;

    void Start()
    {
        dialogueSFX = GetComponent<AudioSource>();
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogue)
        {
            if (!dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (Input.GetButtonDown("Fire1") && dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialogueBox.SetActive(false);
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
            DialogueAnimation.SetTrigger("CloseDialogue");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDialogue = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
