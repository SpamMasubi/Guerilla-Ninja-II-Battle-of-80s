using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class enemyTurret : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;

    public float Range;
    public Transform Target;
    public Vector3 offset;
    bool Detected = false;
    Vector2 Direction;
    public GameObject gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform shootPoint;
    public GameObject Explosion;
    float nextTimeToSearch = 0;

    private void Start()
    {
        FindPlayer();
    }

    void Update()
    {
        if (Target == null)
        {
            FindPlayer();
            return;
        }
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
            if (gameObject.GetComponent<Transform>().localScale.y == 1)
            {
                gun.transform.up = Direction;
            }
            else
            {
                gun.transform.up = -Direction;
            }

            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullets = Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "playerProjectiles" || collision.tag == "playerAttack")
        {
            FindObjectOfType<GameManager>().scores += Random.Range(1, 100);
            ExplosionEffect();
            Destroy(gameObject);
        }
    }

    //Function to instantiate explosion
    void ExplosionEffect()
    {
        AudioManager.instance.PlaySFX("explosion");
        //instiantiate explosion effect
        GameObject explode = (GameObject)Instantiate(Explosion);

        //set explosion position
        explode.transform.position = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position - offset, Range);
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
            if (searchPlayer != null)
                Target = searchPlayer.transform;
            nextTimeToSearch = Time.time + 0.2f;
        }
    }

}
