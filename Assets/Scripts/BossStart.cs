using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public static bool startBoss;

    public GameObject exitClosed;

    public AudioClip startBossSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (FindObjectOfType<BossVehicle>() != null)
            {
                FindObjectOfType<BossVehicle>().GetComponent<Animator>().enabled = true;
                startBoss = true;
            }
            else if (FindObjectOfType<FinalBoss>() != null)
            {
                FindObjectOfType<FinalBoss>().GetComponent<Animator>().enabled = true;
            }
            if (exitClosed != null)
            {
                exitClosed.SetActive(true);
            }
            FindObjectOfType<Canvas>().gameObject.transform.GetChild(4).gameObject.SetActive(true);
            FindObjectOfType<PlayMusic>().PlaySong(FindObjectOfType<PlayMusic>().bossSong);
            AudioManager.instance.SoundObjectCreation(startBossSFX);
            Destroy(gameObject,1f);
        }
    }
}
