using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public static ShootingItem instance;
    public float maxSpeed;
    private float currentSpeed;
    public int damage;

    private void Update()
    {
        if(FindObjectOfType<Player>().isRunning)
        {
            currentSpeed = maxSpeed * 2;
        }
        else
        {
            currentSpeed = maxSpeed;
        }

        transform.Translate(transform.right * transform.localScale.x * currentSpeed * Time.deltaTime);
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
            if (BossStart.startBoss)
            {
                FindObjectOfType<BossHealthBar>().LoseHealth(damage);
            }
            Destroy(gameObject);
        }
    }
}
