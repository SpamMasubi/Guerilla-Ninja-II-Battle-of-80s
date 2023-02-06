using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] players;
    private int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        switch (MainMenu.characterNum)
        {
            case 1:
                playerNum = 1;
                break;
            case 2:
                playerNum = 2;
                break;
            case 3:
                playerNum = 3;
                break;
            default:
                break;
        }
        Instantiate(players[playerNum - 1], gameObject.transform.position, gameObject.transform.rotation);
    }
}
