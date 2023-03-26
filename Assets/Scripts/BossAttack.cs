using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletLauncher;

    public float agroRange;
    public GameObject player;

    public float fireRate;
    public string attackSFX;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0;
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossStart.startBoss && !BossVehicle.isDead)
        {
            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distToPlayer > agroRange || distToPlayer < agroRange)
            {
                CheckIfTimeToFire();
            }
        }
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire && (transform.position.x > player.transform.position.x))
        {
            AudioManager.instance.PlaySFX(attackSFX);
            Instantiate(bullet, bulletLauncher.position, bulletLauncher.rotation);
            nextFire = Time.time + fireRate;
        }
    }
}
