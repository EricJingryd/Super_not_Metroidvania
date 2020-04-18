using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destructable"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
