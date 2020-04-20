using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour
{
    public GameObject HitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(HitEffect, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("PlayerOnTriggerEnemy");
            Destroy(gameObject);
        }                     
    }

    private void Update()
    {
        Destroy(gameObject, 1);
    }
}
