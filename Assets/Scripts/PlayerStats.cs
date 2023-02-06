using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text scores, shurikenCounter, SpACounter;
    public Image mugshot;
    public Sprite[] ninjaCharMugshots;

    private void Start()
    {
        switch (MainMenu.characterNum)
        {
            case 1:
                mugshot.sprite = ninjaCharMugshots[0];
                break;
            case 2:
                mugshot.sprite = ninjaCharMugshots[1];
                break;
            case 3:
                mugshot.sprite = ninjaCharMugshots[2];
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePlayerStatsUI();
    }

    public void UpdatePlayerStatsUI()
    {
        scores.text = "Score: " + FindObjectOfType<GameManager>().scores.ToString("0000000");
        shurikenCounter.text = "x" + FindObjectOfType<GameManager>().shurikenCount.ToString("00");
        SpACounter.text = "x" + FindObjectOfType<GameManager>().SpecialAmmo.ToString("00");
    }
}
