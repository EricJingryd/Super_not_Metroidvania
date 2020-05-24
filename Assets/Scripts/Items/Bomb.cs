using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject destroyEffect;
    public Transform bomb;
    public Transform holdpoint;
    public float bombRadius;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destructable") || other.CompareTag("Enemy"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, bombRadius);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].gameObject.CompareTag("Destructable") || objectsToDamage[i].gameObject.CompareTag("Enemy"))
                {
                    Destroy(objectsToDamage[i].gameObject);
                    Destroy(gameObject);
                    FindObjectOfType<AudioManager>().Play("BombExplosionSound");
                }

            }
        }
        else if (other.CompareTag("EnemyBoss"))
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("BombExplosionSound");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
