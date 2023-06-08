using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FinalBoss : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform player;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    [SerializeField] LayerMask jumpableGround;
    [SerializeField] Transform groundChecker;
    const float groundCheckRadius = 0.25f;

    bool isGrounded; //ground checker

    private Path path;
    private int currentWayPoint = 0;
    Seeker seeker;
    private Rigidbody2D enemyRB;
    private Animator enemyAnim;
    //Other
    [Header("Final Boss Attributes")]
    public static bool isDead = false; //check if player is Dead
    public static bool stageClear = false;
    public Transform bulletLauncher;
    public GameObject superPowers;

    [Header("Invincibility Flash")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public static bool isInvincible;
    private bool isAttack;
    private bool isHurt;
    public SpriteRenderer bossSprite;

    public string bossName;
    public int bossHealth = 50;
    float maxHealth;
    public static bool gameComplete;
    public AudioClip startPhase2, deadsfx, invincible, secondPhaseMusic;
    public float attackTime = 0f;
    float attackTimer = 0f;
    public static bool beginPhase2;
    bool faceDirection;
    bool hasStarted;

    public AudioClip[] ninjaCharVictory;
    private AudioClip victorySFX;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemyRB = GetComponent<Rigidbody2D>();
        maxHealth = bossHealth;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        enemyAnim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        bossSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        if (BossStart.startBoss && !isDead)
        {
            if (TargetInDistance() && followEnabled && !isAttack && !isHurt)
            {
                PathFollow();
            }
            else
            {
                enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
            }
        }
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(enemyRB.position, player.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        //Reached end of path
        if (currentWayPoint >= path.vectorPath.Count)
        {
            return;
        }

        //Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - enemyRB.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Jump
        if (jumpEnabled && GroundCheck())
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                enemyRB.AddForce(Vector2.up * speed * jumpModifier);
                enemyAnim.SetBool("Jump", true);
            }
            else
            {
                enemyAnim.SetBool("Jump", false);
            }
        }

        //Movement
        if (!GroundCheck())
        {
            force.y = 0;
        }
        else
        {
            enemyRB.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        }
        

        if (enemyRB.velocity.x > speed)
        {
            enemyRB.velocity = new Vector2(speed, enemyRB.velocity.y);
        }
        else if (enemyRB.velocity.x < speed * (-1))
        {
            enemyRB.velocity = new Vector2(speed * (-1), enemyRB.velocity.y);
        }

        //NextWaypoint
        float distance = Vector2.Distance(enemyRB.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        //Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (enemyRB.velocity.x > 0.05f)
            {
                faceDirection = true;
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (enemyRB.velocity.x < -0.05f)
            {
                faceDirection = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private bool GroundCheck() //Check if Groundcheck is colliding with Ground Layered object
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, groundCheckRadius, jumpableGround);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        return isGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && bossHealth <= (maxHealth/2) && !beginPhase2)
        {
            isInvincible = true;
            beginPhase2 = true;
            FindObjectOfType<PlayMusic>().StopSong();
            AudioManager.instance.SoundObjectCreation(startPhase2);
            enemyAnim.SetTrigger("Second Phase");
        }

        if(BossStart.startBoss && !hasStarted)
        {
            enemyAnim.SetTrigger("BeginBoss");
            hasStarted = true;
        }

        if (BossStart.startBoss && !isDead)
        {
            if (attackTimer <= 0f)
            {
                attackTimer = attackTime;
            }
            if (attackTimer > 0f)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    isAttack = true;
                    if (beginPhase2)
                    {
                        int chosenAttack = Random.Range(0, 3);
                        if (chosenAttack == 0)
                        {
                            enemyAnim.SetTrigger("Attack 1");
                        }
                        else if(chosenAttack == 1)
                        {
                            enemyAnim.SetTrigger("Attack 2");
                        }
                        else
                        {
                            enemyAnim.SetTrigger("Attack 3");
                        }
                    }
                    else
                    {
                        enemyAnim.SetTrigger("Attack 1");
                    }
                }
            }
        }
    }

    public void Shoot(GameObject bullet)
    {
        GameObject projectile = Instantiate(bullet, bulletLauncher.position, bulletLauncher.rotation);

        if (FindObjectOfType<EnemyBullet>())
        {
            if (!faceDirection)
            {
                projectile.GetComponent<EnemyBullet>().direction = -1;
            }
            else
            {
                projectile.GetComponent<EnemyBullet>().direction = 1;
            }
        }
        else
        {
            if (!faceDirection)
            {
                projectile.GetComponent<HomingMissile>().facing = 1;
            }
            else
            {
                projectile.GetComponent<HomingMissile>().facing = -1;
            }
        }
    }

    void disableSecondPhase()
    {
        superPowers.SetActive(true);
        isInvincible = false;
        isAttack = false;
        isHurt = false;
        attackTime = 3;
        speed = 10;
        jumpModifier = 50;
        FindObjectOfType<PlayMusic>().PlaySong(secondPhaseMusic);
    }

    public void FinalBossSFX(AudioClip sfx)
    {
        AudioManager.instance.SoundObjectCreation(sfx);
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

    public void replayMusicifPlayerDeadInSecondPhase()
    {
        FindObjectOfType<PlayMusic>().PlaySong(secondPhaseMusic);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "playerProjectiles" || collision.tag == "playerAttack")
        {
            if (!isInvincible)
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
                    isHurt = true;
                    StartCoroutine(InvincibilityFlash());
                    AudioManager.instance.PlaySFX("bossDamage");
                    enemyAnim.SetTrigger("Hurt");
                }
            }
            else
            {
                AudioManager.instance.PlaySFX("bossHit");
                AudioManager.instance.SoundObjectCreation(invincible);
            }
        }
    }

    public void BossDead()
    {
        superPowers.SetActive(false);
        FindObjectOfType<GameManager>().scores += Random.Range(100, 1000);
        if (!StageSelection.replayMode)
        {
            FindObjectOfType<GameManager>().yourScore();
            FindObjectOfType<GameManager>().setHighScore();
        }
        AudioManager.instance.SoundObjectCreation(deadsfx);
        isDead = true;
        Destroy(FindObjectOfType<PlayMusic>().gameObject);
        GetComponent<FinalBoss>().enabled = false;
        enemyAnim.SetTrigger("Defeated");
    }

    void Reset()
    {
        isAttack = false;
        isHurt = false;
    }

    public void StageClear()
    {
        PlayerPrefs.SetInt("GameWin", 1);
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
        gameComplete = true;
        beginPhase2 = false;
        hasStarted = false;
    }
   
}
