using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum ItemType {POINTS, AMMUNITIONS, HEALTH, LIFE} //type of interaction
    [Header("Attributes")]
    public ItemType item;
    public int points, healthRestore, ammo, life;
    public Sprite[] ninjaCharLife, specialAmmo;

    public bool isLootItem;

    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;

    bool isGrounded; //ground checker
    public static bool canGetItem;

    private void Start()
    {
        if (item == ItemType.LIFE) {
            switch (MainMenu.characterNum)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = ninjaCharLife[0];
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = ninjaCharLife[1];
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = ninjaCharLife[2];
                    break;
            }
        }
        if(item == ItemType.AMMUNITIONS && gameObject.name == "Special Ammo(Clone)")
        {
            switch (MainMenu.characterNum)
            {
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = specialAmmo[0];
                    break;
            }
        }
    }

    void Reset() //reset function
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
    }

    void FixedUpdate()
    {
        if (isLootItem)
        {
            GroundCheck();
        }
    }

    public void itemTypes() //interaction of items
    {
        switch (item)
        {
            case ItemType.POINTS:
                if (gameObject.name == "Big Money")
                {
                    AudioManager.instance.PlaySFX("Points");
                    FindObjectOfType<GameManager>().scores += points;
                }
                else
                {
                    AudioManager.instance.PlaySFX("Points");
                    FindObjectOfType<GameManager>().scores += points;
                }
                break;
            case ItemType.AMMUNITIONS:
                if (gameObject.name == "Special Ammo" || gameObject.name == "Special Ammo(Clone)")
                {
                    AudioManager.instance.PlaySFX("TakeAmmo");
                    FindObjectOfType<GameManager>().SpecialAmmo += ammo;
                }
                else if (gameObject.name == "Shuriken Ammo" || gameObject.name == "Shuriken Ammo(Clone)")
                {
                    AudioManager.instance.PlaySFX("TakeAmmo");
                    FindObjectOfType<GameManager>().shurikenCount += ammo;

                }
                break;
            case ItemType.HEALTH:
                if(gameObject.name == "Bun Rieu")
                {
                    FindObjectOfType<Healthbar>().gainHealth(healthRestore);
                }
                else
                {
                    FindObjectOfType<Healthbar>().gainHealth(healthRestore);
                }
                break;
            case ItemType.LIFE:
                AudioManager.instance.PlaySFX("LifeRestore");
                FindObjectOfType<GameManager>().lives += 1;
                break;
            default:
                Debug.Log("Null");
                break;
        }

    }

    void GroundCheck() //Check if Groundcheck is colliding with Ground Layered object
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLootItem)
        {
            if (collision.gameObject.tag == "Player")
            {
                itemTypes();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isLootItem)
        {
            if (collision.gameObject.tag == "Player" && isGrounded)
            {
                itemTypes();
                canGetItem = true;
                Destroy(gameObject);
            }
        }
    }
}
