using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTripleBeam : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //FindObjectOfType<CharacterController2D>().playerHasTripleBeam = true;
            FindObjectOfType<Weapon>().playerHasTripleBeam = true;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Upgrade");
        }
    }
}
