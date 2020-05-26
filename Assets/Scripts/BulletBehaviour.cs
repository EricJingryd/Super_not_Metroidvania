using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D bulletRB;
    public GameObject HitEffect;
    [SerializeField] float bulletLifeTime = 1f;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //bulletRB.velocity = transform.right * speed;
        bulletRB.velocity = Vector2.right * player.transform.localScale.x * speed;
    }

    void Update()
    {
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBoss"))
        {
            Instantiate(HitEffect, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("PlayerOnTriggerEnemy");
            Destroy(gameObject);
        }
    }
}
