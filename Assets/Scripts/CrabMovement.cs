using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    [Header("Crab Parameters")]
    [SerializeField] float moveSpeed = 3f;

    //Cached component references
    Rigidbody2D crabRigidBody;
    BoxCollider2D crabBoxCollider;
    
    void Start()
    {
        crabRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingLeft())
        {
            crabRigidBody.velocity = new Vector2(-moveSpeed, 0);
        }
        else
        {
            crabRigidBody.velocity = new Vector2(moveSpeed, 0);
        }
    }

    bool IsFacingLeft()
    {
        return transform.localScale.x < 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (!crabBoxCollider.)
        transform.localScale = new Vector2(-(Mathf.Sign(crabRigidBody.velocity.x)), 1f);
    }
}
