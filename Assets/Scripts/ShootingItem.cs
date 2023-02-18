using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public static ShootingItem instance;
    public float maxSpeed;
    private float currentSpeed;
    public int damage;
    int direction = 1;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (!Player.facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            direction = -1;
        }
    }

    private void Update()
    {
        if (FindObjectOfType<Player>().isRunning)
        {
            currentSpeed = maxSpeed * 2;
        }
        else
        {
            currentSpeed = maxSpeed;
        }

        rb2d.velocity = new Vector3(currentSpeed * direction, 0, 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            return;
        }
        //Destroy
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (BossStart.startBoss && BossVehicle.hitBoxAppear && !BossVehicle.isInvincible)
            {
                FindObjectOfType<BossHealthBar>().LoseHealth(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
