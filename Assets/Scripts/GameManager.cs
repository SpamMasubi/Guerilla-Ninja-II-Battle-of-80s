using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 3;
    public float health = 100;
    public static bool unlockReplay = false;

    [HideInInspector]public int scores = 0, shurikenCount = 3, SpecialAmmo = 10;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameWin();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void setHighScore()
    {
        int HS = PlayerPrefs.GetInt("High Score");
        if (scores <= HS)
        {
            return;
        }
        PlayerPrefs.SetInt("High Score", scores);
    }

    public void yourScore()
    {
        PlayerPrefs.SetInt("Your Score", FindObjectOfType<GameManager>().scores);
    }

    public bool gameWin()
    {
        int isGameWin = PlayerPrefs.GetInt("GameWin");
        if(isGameWin == 1)
        {
            unlockReplay = true;
            return true;
        }
        else
        {
            unlockReplay = false;
            return false;
        }
    }

    public void ResetAmmo()
    {
        shurikenCount = 3;
        SpecialAmmo = 10;
    }
}
