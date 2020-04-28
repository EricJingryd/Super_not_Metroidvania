using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpeedBuff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<CharacterController2D>().playerHasSpeedBuff = true;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Upgrade");
        }
    }
}
