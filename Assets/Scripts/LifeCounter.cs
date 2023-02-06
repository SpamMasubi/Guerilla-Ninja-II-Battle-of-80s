using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Text lives;
    public Image lifeImage;
    public Sprite[] ninjaCharLife;
    public void Start()
    {
        switch (MainMenu.characterNum)
        {
            case 1:
                lifeImage.sprite = ninjaCharLife[0];
                break;
            case 2:
                lifeImage.sprite = ninjaCharLife[1];
                break;
            case 3:
                lifeImage.sprite = ninjaCharLife[2];
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        UpdateLives(FindObjectOfType<GameManager>().lives); //update lives UI
    }

    public void LoseLife()
    {
        //if no lives remaining, return
        if(FindObjectOfType<GameManager>().lives == 0)
        {
            return;
        }
        //Decrease the value of livesRemaining
        FindObjectOfType<GameManager>().lives--;
    }

    public void UpdateLives(int remainingLives)
    {
        //Convert lives remaining number to text
        lives.text = "x" + remainingLives.ToString("00");
    }

}
