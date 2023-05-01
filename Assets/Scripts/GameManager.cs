using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 3;
    public float health = 100;

    [HideInInspector]public int scores = 0, shurikenCount = 3, SpecialAmmo = 10;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    public void ResetAmmo()
    {
        scores = 0;
        shurikenCount = 3;
        SpecialAmmo = 10;
    }
}
