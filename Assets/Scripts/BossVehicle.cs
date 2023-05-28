using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVehicle : MonoBehaviour
{
    //Reference to waypoints
    public List<Transform> points;
    //The int value for the next point index
    int nextID;
    //The value of that applies to the ID for changing
    int idChangeValue = 1;
    //Speed of movement or flying
    public float speed = 5;
    public string bossName;
    public float bossHealth = 0f;
    public AudioClip startPhase2, deadsfx;
    public GameObject hitBox;
    public float hitBoxAppearTime = 0f;
    public GameObject mainGun;
    public bool aerialBoss;

    float hitBoxAppearTimer = 0f;
    public static bool hitBoxAppear = true;
    float maxHealth;

    public static bool isDead = false; //check if player is Dead
    public static bool stageClear = false;
    bool beginPhase2;
    bool beginMoving;

    Animator anim;

    public GameObject Explosion;

    [Header("Invincibility Flash")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public static bool isInvincible;
    public SpriteRenderer bossSprite;
    public AudioClip[] ninjaCharVictory;
    private AudioClip victorySFX;

    void Start()
    {
        anim = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();
        maxHealth = bossHealth;
    }

    private void Reset()
    {
        Init();
    }

    void Init()
    {

        GameObject root = new GameObject(name + "_Root");
        //Reset Position of Root to this Boss
        root.transform.position = transform.position;
        //Set enemy object as child of root
        transform.SetParent(root.transform);
        //Create Waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //Reset waypoints position to root
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //Create two points and reset their position to waypoint objects
        //Make the points children of waypoint object
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        //Init point lists then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    private void Update()
    {
        if (BossStart.startBoss && beginMoving)
        {
            MoveToNextPoint();
        }

        if (!isDead && bossHealth <= (maxHealth / 2) && !beginPhase2)
        {
            isInvincible = true;
            beginPhase2 = true;
            AudioManager.instance.SoundObjectCreation(startPhase2);
            anim.SetBool("Second Phase", true);
        }

        //Special attack for boss
        if (beginPhase2 && hitBox != null && !hitBoxAppear)
        {
            if (hitBoxAppearTimer <= 0f)
            {
                hitBoxAppearTimer = hitBoxAppearTime;
            }
            if (hitBoxAppearTimer > 0f)
            {
                hitBoxAppearTimer -= Time.deltaTime;
                if (hitBoxAppearTimer <= 0f)
                {
                    anim.SetTrigger("enableHitBox");
                }
            }
        }
    }


    public void MoveToNextPoint()
    {
        //Get the next Point transform
        Transform goalPoint = points[nextID];

        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        //Check the distance between the enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            //check if we are at the end of the line(make the change -1)
            if (nextID == points.Count - 1)
            {
                idChangeValue = -1;
            }
            //check if we are at the start of the line(make the change +1)
            if (nextID == 0)
            {
                idChangeValue = 1;
            }
            //Apply the change on the nextID
            nextID += idChangeValue;
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        int temp = 0;
        isInvincible = true;
        while (temp < numberOfFlashes)
        {
            bossSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            bossSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "playerProjectiles" || collision.tag == "playerAttack")
        {
            if (!isInvincible && hitBoxAppear)
            {
                if (collision.tag == "playerAttack")
                {
                    FindObjectOfType<BossHealthBar>().LoseHealth(1);
                }
                else
                {
                    FindObjectOfType<ShootingItem>().takeDamage();
                }

                if (bossHealth > 0)
                {
                    StartCoroutine(InvincibilityFlash());
                    AudioManager.instance.PlaySFX("bossDamage");
                    anim.SetTrigger("Hurt");
                }
            }
            else
            {
                AudioManager.instance.PlaySFX("bossHit");
            }
        }
    }

    void cantHit()
    {
        hitBoxAppear = false;
        isInvincible = true;
        if (aerialBoss)
        {
            mainGun.SetActive(false);
        }
        GetComponentInChildren<BossAttack>().enabled = false;
    }

    void startPhaseTwo()
    {
        if (aerialBoss)
        {
            FindObjectOfType<BossVehicle>().GetComponentInChildren<BossSpecial>().enabled = true;
            GetComponent<BossSpecial>().enabled = true;
            gameObject.transform.GetChild(3).GetComponent<BossSpecial>().enabled = true;
            GetComponent<AudioSource>().enabled = true;
        }
        else if (gameObject.name == "Cuban BTR (Third Boss)")
        {
            GetComponent<BossSpecial>().enabled = true;
            GetComponent<turretScript>().enabled = true;
        }
        else
        {
            GetComponent<turretScript>().enabled = true;
        }
        isInvincible = false;
        beginMoving = true;
        hitBoxAppear = false;
    }

    public void enableHitBox()
    {
        if (aerialBoss)
        {
            mainGun.SetActive(true);
        }
        else
        {
            FindObjectOfType<BossVehicle>().GetComponentInChildren<BossAttack>().enabled = true;
            if (gameObject.name == "Cuban BTR (Third Boss)")
            {
                GetComponent<BossSpecial>().enabled = false;
            }
            else
            {
                GetComponent<turretScript>().enabled = false;
            }
        }
        hitBoxAppear = true;
        beginMoving = false;
    }

    public void BossDead()
    {
        isDead = true;
        ExplosionEffect();
        anim.SetTrigger("Defeated");
        FindObjectOfType<GameManager>().scores += Random.Range(100, 1000);
        if (aerialBoss)
        {
            GetComponent<AudioSource>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0.03f;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<BossSpecial>().enabled = false;
            gameObject.transform.GetChild(3).GetComponent<BossSpecial>().enabled = false;
        }
        else
        {
            FindObjectOfType<BossVehicle>().GetComponentInChildren<BossSpecial>().enabled = false;
        }
        Destroy(FindObjectOfType<PlayMusic>().gameObject);
        AudioManager.instance.SoundObjectCreation(deadsfx);
        GetComponent<BossVehicle>().enabled = false;
        if (mainGun != null)
        {
            Destroy(mainGun);
        }
    }

    //Function to instantiate explosion
    void ExplosionEffect()
    {
        //instiantiate explosion effect
        GameObject explode = (GameObject)Instantiate(Explosion);

        //set explosion position
        explode.transform.position = transform.position;
    }

    public void damageEnemySFX(AudioClip sfx)
    {
        if (aerialBoss)
        {
            mainGun.SetActive(false);
        }
        AudioManager.instance.SoundObjectCreation(sfx);
    }

    public void StageClear()
    {
        anim.SetBool("Second Phase", false);
        ChapterIntro.chapters += 1;
        stageClear = true;
        switch (MainMenu.characterNum)
        {
            case 1:
                victorySFX = ninjaCharVictory[0];
                break;
            case 2:
                victorySFX = ninjaCharVictory[1];
                break;
            case 3:
                victorySFX = ninjaCharVictory[2];
                break;
        }
        AudioManager.instance.SoundObjectCreation(victorySFX);
    }
}
