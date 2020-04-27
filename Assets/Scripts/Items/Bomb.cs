using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject destroyEffect;
    public float bombRadius;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destructable"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, bombRadius);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].gameObject.CompareTag("Destructable"))
                {
                    Destroy(objectsToDamage[i].gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
