using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Enemy : MonoBehaviour
{
    public HealthBar healthBar;

    public float speed;
    public float distance;
    public int hitpoints;
    public int maxHitpoints;

    public bool movingRight = true;

    public Transform groundDetection;
    private void Start()
    {
        hitpoints = maxHitpoints;
        healthBar.SetMaxHealth(maxHitpoints);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,distance);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
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
                FindObjectOfType<AudioManager>().Play("JumperDeath");
            }
        }
    }
}
