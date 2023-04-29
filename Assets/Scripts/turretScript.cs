using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    public float Range;
    public Transform Target;
    public Vector3 offset;
    bool Detected;
    Vector2 Direction;
    public GameObject gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform shootPoint;
    public bool GroundBossType;

    private void Start()
    {
        Target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (BossStart.startBoss && !BossVehicle.isDead)
        {
            Vector3 targetPos = Target.position;
            Direction = targetPos - (Vector3)transform.position;
            Vector3 pos = transform.position;
            pos += transform.right * -offset.x;
            pos += transform.up * -offset.y;
            Collider2D[] rayInfo = Physics2D.OverlapCircleAll(pos, Range, 1 << LayerMask.NameToLayer("Player")); //rayInfo will be true only if hit the player.
            if (rayInfo.Length > 0)
            {
                if (Detected == false)
                {
                    Detected = true;
                }
            }
            else
            {
                if (Detected == true)
                {
                    Detected = false;
                }
            }

            if (Detected == true)
            {
                if (FindObjectOfType<BossVehicle>() != null)
                {
                    if (GroundBossType)
                    {
                        if (offset.x < 0)
                        {
                            gun.transform.right = Direction;
                        }
                        else
                        {
                            gun.transform.right = -Direction;
                        }
                    }
                    else
                    {
                        gun.transform.up = -Direction;
                    }
                }
                else if (FindObjectOfType<FinalBoss>() != null)
                {
                    gun.transform.right = -Direction;
                }

                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    Shoot(); 
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullets = Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position - offset, Range);
    }

}
