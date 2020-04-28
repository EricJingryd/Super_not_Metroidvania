using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDoubleJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<CharacterController2D>().playerHasDoubleJump = true;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Upgrade");
        }
    }
}
