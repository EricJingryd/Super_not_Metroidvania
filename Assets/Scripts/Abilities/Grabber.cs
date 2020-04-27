using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public float throwforce;
    public Transform Holdpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!grabbed)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance); //Uses raycast in front of player to check for collision

                if (hit.collider != null && hit.collider.gameObject.CompareTag("Bomb"))
                {
                    grabbed = true;
                }
            }

            else
            {
                grabbed = false;

                hit.collider.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
                hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                hit.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) //Throws object
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
                }
            }
        }

        if (grabbed) //Keeps the object at a point that is always above player
        {
            hit.collider.gameObject.transform.position = Holdpoint.position;
            hit.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            hit.collider.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnDrawGizmos() //Visual aid
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x*distance);
    }
}
