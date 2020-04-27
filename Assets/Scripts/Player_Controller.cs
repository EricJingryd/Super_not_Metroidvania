using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour

{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40;

    private float horizontalMovement = 0f;

    private bool jumpFlag = false;
    private bool shooting = false;
    private bool jump = false;
    private bool crouch = false;

    void Update()
    {

        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            //animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shooting = true;
            animator.SetBool("IsShooting", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            shooting = false;
            animator.SetBool("IsShooting", false);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMovement * Time.fixedDeltaTime, crouch, jump);
        //jump = false;
        if (jump)
        {
            jumpFlag = true;
            jump = false;
        }
    }
}
