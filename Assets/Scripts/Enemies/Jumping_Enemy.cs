using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Enemy : MonoBehaviour
{
    private Rigidbody2D jumpingEnemy;

    public float speed;
    public float jumpForce = 300f;
    public float jumpRangeOne = 1750;
    public float jumpRangeTwo = 2500;

    public int hitpoints;
    public int maxHitpoints;

    public GameObject EnemyDeathEffect;
    public HealthBar healthBar;
    // Start is called before the first frame update

    void Awake()
    {
        jumpingEnemy = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine (Jump ());
        hitpoints = maxHitpoints;
        healthBar.SetMaxHealth(maxHitpoints);
    }

    // Update is called once per frame
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        jumpForce = Random.Range(jumpRangeOne, jumpRangeTwo);
        jumpingEnemy.AddForce(new Vector2(0f, jumpForce));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Jump());
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
