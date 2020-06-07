using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Enemy : MonoBehaviour
{
    private Rigidbody2D jumpingEnemy;

    public float speed;
    public float jumpForce;
    private float dir;

    public int hitpoints;
    public int maxHitpoints;

    public GameObject EnemyDeathEffect;
    Transform player;
    public HealthBar healthBar;
    // Start is called before the first frame update

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jumpingEnemy = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine (Jump ());
        hitpoints = maxHitpoints;
        healthBar.SetMaxHealth(maxHitpoints);
    }
    private void Update()
    {
    }

    // Update is called once per frame
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(2);
        if (transform.position.x - player.position.x < 0)
            dir = 1;
        if (transform.position.x - player.position.x > 0)
            dir = -1;
        jumpingEnemy.AddForce(new Vector2(10*dir, jumpForce),ForceMode2D.Impulse);
        
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
