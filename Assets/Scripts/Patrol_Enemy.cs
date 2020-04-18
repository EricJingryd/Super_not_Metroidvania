using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Enemy : MonoBehaviour
{
    public float speed;
    public float distance;
    public float hitpoints = 1;

    public bool movingRight = true;

    public Transform groundDetection;
    public GameObject EnemyDeathEffect;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,distance);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
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
                Instantiate(EnemyDeathEffect, transform.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("JumperDeath");
                FindObjectOfType<AudioManager>().Play("EnemyExplosionSound");
            }
        }
    }
}
