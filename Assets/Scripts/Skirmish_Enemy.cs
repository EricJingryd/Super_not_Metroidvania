﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skirmish_Enemy : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float hitpoints = 1;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;
 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }


    void Update()
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
       
        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot"))
        {
            hitpoints -= 1;
            Debug.Log("test");
            if (hitpoints <= 0)
            {
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("JumperDeath");
            }
        }
    }
}
