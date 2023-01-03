using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake()
    {
        instance = this;
    }

    //Sound Effects
    public AudioClip jump, run, points,
        shootAR, shootHandgun, shootSniper, throwing, explosion, enemyHit,
        health, ammo, life, switchWeapon, bossHit, bossDamage;
    //Music
    public AudioClip music;

    //Sound Object
    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "jump":
                SoundObjectCreation(jump);
                break;
            case "Points":
                SoundObjectCreation(points);
                break;
            case "running":
                SoundObjectCreation(run);
                break;
            case "throwing":
                SoundObjectCreation(throwing);
                break;
            case "shootHandgun":
                SoundObjectCreation(shootHandgun);
                break;
            case "shootAssaultRifle":
                SoundObjectCreation(shootAR);
                break;
            case "shootSniper":
                SoundObjectCreation(shootSniper);
                break;
            case "explosion":
                SoundObjectCreation(explosion);
                break;
            case "enemyHit":
                SoundObjectCreation(enemyHit);
                break;
            case "bossHit":
                SoundObjectCreation(bossHit);
                break;
            case "bossDamage":
                SoundObjectCreation(bossDamage);
                break;
            case "HealthRestore":
                SoundObjectCreation(health);
                break;
            case "TakeAmmo":
                SoundObjectCreation(ammo);
                break;
            case "LifeRestore":
                SoundObjectCreation(life);
                break;
            case "weaponSwitch":
                SoundObjectCreation(switchWeapon);
                break;
            default:
                break;
        }
    }

    public void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign aduioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }

}
