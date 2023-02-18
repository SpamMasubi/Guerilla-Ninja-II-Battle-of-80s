using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeapons : MonoBehaviour
{
    public Image[] playerWeapons;
    public Sprite[] weaponsBasedOnChar;

    public static bool shuriken = true, special;

    private float rgb = 0.3301887f;

    private void Start()
    {
        switch (MainMenu.characterNum)
        {
            case 1:
                playerWeapons[0].sprite = weaponsBasedOnChar[0];
                playerWeapons[1].sprite = weaponsBasedOnChar[1];
                break;
            case 2:
                playerWeapons[0].sprite = weaponsBasedOnChar[2];
                playerWeapons[1].sprite = weaponsBasedOnChar[3];
                break;
            case 3:
                playerWeapons[0].sprite = weaponsBasedOnChar[4];
                playerWeapons[1].sprite = weaponsBasedOnChar[5];
                break;
            default:
                break;
        }

        if (shuriken)
        {
            shurikenSelect();
        }
        else
        {
            specialSelected();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPause && !BossVehicle.isDead && !FinalBoss.isDead)
        {
            //Controller
            if (Input.GetButtonDown("Debug Next"))
            {
                AudioManager.instance.PlaySFX("weaponSwitch");
                if (shuriken)
                {
                    specialSelected();
                }
                else
                {
                    shurikenSelect();
                }
            }
            else if (Input.GetButtonDown("Debug Previous"))
            {
                AudioManager.instance.PlaySFX("weaponSwitch");
                if (special)
                {
                    shurikenSelect();
                }
                else
                {
                    specialSelected();
                }
            }

            //Keyboards
            if (Input.GetKeyDown(KeyCode.Alpha1)) //if alpha1, shuriken available
            {
                AudioManager.instance.PlaySFX("weaponSwitch");
                shurikenSelect();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AudioManager.instance.PlaySFX("weaponSwitch");
                specialSelected();
            }
        }
    }

    void shurikenSelect()
    {
        shuriken = true;
        playerWeapons[0].color = new Color(1, 1, 1);

        special = false;
        playerWeapons[1].color = new Color(rgb, rgb, rgb);
    }

    void specialSelected()
    {
        special = true;
        playerWeapons[1].color = new Color(1, 1, 1);

        shuriken = false;
        playerWeapons[0].color = new Color(rgb, rgb, rgb);
    }
}
