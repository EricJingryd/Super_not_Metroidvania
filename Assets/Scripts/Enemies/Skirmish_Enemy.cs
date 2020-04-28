using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skirmish_Enemy : MonoBehaviour
{
    public HealthBar healthBar;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public int hitpoints;
    public int maxHitpoints = 1;
    public int range;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject EnemyDeathEffect;
    public GameObject projectile;
    public Transform player;
 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        hitpoints = maxHitpoints;
        healthBar.SetMaxHealth(maxHitpoints);
    }


    void Update()
    {
        if (player != null) // Finns pga att när spelar transformen försvinner, så kmr inte  
        {

            if (Vector2.Distance(player.position,transform.position) <= range)
            {
                if(Vector2.Distance(transform.position,player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                }

                else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }
        

                if (timeBtwShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot"))
        {
            hitpoints -= 1;
            healthBar.SetHealth(hitpoints);
            if (hitpoints <= 0)
            {
                Destroy(gameObject);
                Instantiate(EnemyDeathEffect, transform.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("JumperDeath");
                FindObjectOfType<AudioManager>().Play("EnemyExplosionSound");
            }
        }
    }
}
