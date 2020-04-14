using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [Header("Configuration Parameters")]        //Rubrik för allmänna konfigurationsparametrar

    [Header("Speed Parameters")]                //Rubrik för hastighetskonfiguration (i Unity:s interface)
    [SerializeField] float runSpeed = 5f;       //Fält för löphastighet, gångras med "controlThrow" i Run() nedan
    [SerializeField] float jumpSpeed = 5f;      //Fält för hopphastighet
    [SerializeField] float climbSpeed = 5f;     //Fält för klättringshastighet

    [Header("Projectile Parameters")]                       //Rubrik för projektilparametrar
    [SerializeField] GameObject playerShotPrefab;           //Fält för spelarens skott
    [SerializeField] float projectileSpeed = 10f;           //Fält för skotthastighet
    [SerializeField] float projectileFiringPeriod = 0.1f;   //Fält för skottfrekvens
    [SerializeField] bool Shooting=false;


    [Header("Player Hitpoints")]
    [SerializeField] float hitpoints = 3;

    Coroutine firingCoroutine;      //Deklareras för att kunna stoppa enskilda Coroutines istället för alla Coroutines i "Player.cs" - Se "Fire()"

    //States - Tillstånd i spelet
    bool isAlive = true;
    bool playerIsGrounded;  //Används senare för landningsanimation
    bool playerHasHorizontalSpeed;
    bool playerHasVerticalSpeed;
    bool playerHasJumpSpeed;
    public bool playerHasJumpBoots { get; set; }
    bool playerCanDoubleJump = false;

    //Cached component references - Lagrad(e) data/referenser
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider2D;
    BoxCollider2D playerFeetCollider2D;

    float gravityScaleAtStart;

    //Message, then methods

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        ClimbLadder();
        Jump();
        DoubleJump();
        FlipSprite();
        Fire();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");   //-1 till +1, horisontell rörelse mha. Unity rörelsefunktion
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y); //y-vektorns hastighet anges med myRigiBody för att låta den göra det den gör
        playerRigidBody.velocity = playerVelocity;

        playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;  //Detta är en if-sats som kollar om det absoluta värdet på horisontell
                                                                                            //rörelse är större än 0 (Epsilon). Är den det så returneras True. Att 
                                                                                            //detta inte skrivs i if-satsen nedan beror på bättre readability.
        playerAnimator.SetBool("Run", playerHasHorizontalSpeed);    //Ändrar state till Run om spelaren springer, löpanimation startar
    }

    private void ClimbLadder()
    {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("Climbing", false);  //Om spelaren inte krockar med stegen så spelas inte klättringsanimationen upp
            playerRigidBody.gravityScale = gravityScaleAtStart;
            return; //Return om spelaren inte kolliderar med layern
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        playerRigidBody.gravityScale = 0f;  //Om spelaren klättrar sätts 0 gravitation för att denne inte ska falla

        playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;    //Detta är en if-sats som kollar om det absoluta värdet på vertikal
                                                                                            //rörelse är större än 0 (Epsilon). Är den det så returneras True. Att 
                                                                                            //detta inte skrivs i if-satsen nedan beror på bättre readability.
        playerAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Jumpable"))) { return; }    //Return om spelaren inte kolliderar med layern

        if (CrossPlatformInputManager.GetButtonDown("Jump") || (CrossPlatformInputManager.GetButtonDown("Jump") && CrossPlatformInputManager.GetButtonDown("Vertical")))    //CrossPlatformInputManager underlättar för att spela spelet över olika plattformar
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed); //Lägger till hopphastigheten när hoppaknappen trycks ned
            playerRigidBody.velocity += jumpVelocityToAdd;
            playerHasJumpSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;

            playerAnimator.SetBool("Jump", playerHasJumpSpeed);

        /*  Här behövs landingsanimation och kod för den  */

            //playerAnimator.SetBool("Run", false);
            //playerAnimator.SetBool("Jump", playerHasJumpSpeed);

            playerCanDoubleJump = true;
        }
    }

    private void DoubleJump()
    {
        if (!playerBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Jumpable")) && playerHasJumpBoots) //Kollar att spelaren är i luften och har jumpboots
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump") && playerCanDoubleJump)
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                playerRigidBody.velocity = Vector2.zero; //Sätter velocity till noll annars blir hopp avstånde/hastighet olika beroende på när man trycker jump
                playerRigidBody.velocity += jumpVelocityToAdd;

                playerCanDoubleJump = false; //Så att spelaren inte kan hoppa om och om igen

                FindObjectOfType<AudioManager>().Play("PlayerJump");
            }
        }
    }

    private void FlipSprite()   //Flippa spriten om spelaren rör sig åt olika håll i horisontell riktning
    {
        /*bool */playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;  //Detta är en if-sats som kollar om det abosluta värdet på horisontell
        //rörelse är större än 0 (Epsilon). Är den det så returneras True. Att detta inte skrivs i if-satsen nedan beror på bättre readability.
        if (playerHasHorizontalSpeed) //Om detta är sant:
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f); //Flippa signen som antingen är -1 eller +1 mha. Mathf. --> Slipper cacha värden 
            //vilket tar mindre prestanda, då den enbart kontrollerar hastigheten när den byts.
        }
    }

    private void Fire() //Mycket kommer utan tvekan att läggas till och ändras häri :) 
    {
        if (Input.GetButtonDown("FireGun") && !Shooting)
        {
            FindObjectOfType<AudioManager>().Play("PlayerShot");
            firingCoroutine = StartCoroutine(FireContinously());
            Debug.Log("test1");
            Shooting = true;
        }
        if (Input.GetButtonUp("FireGun"))
        {
            StopCoroutine(firingCoroutine);
            Shooting = false;
            Debug.Log("test2");
        }
    }

    IEnumerator FireContinously()   //Coroutine för att skjuta kontinuerligt genom att hålla ner knappen
    {                                       //Buggigt just nu med skottets riktning, men det kommer ändå att ändras i framtiden så jag struntar i det nu
        while (true)
        {
            GameObject playerShot = Instantiate(
            playerShotPrefab,
            transform.position,
            Quaternion.identity)
            as GameObject; //Spawnar skottet (vid spelarens position för tillfället) som ett GameObject
            //if (playerHasHorizontalSpeed)
            //{
            //    projectileSpeed *= -1;  //Om spelaren står eller rör sig åt vänster skickas skottet åt det hållet med. Skickar alltså bara iväg
            //}                           //skottet, men det är spelarens riktning som bestämmer vart det åker.
            playerShot.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * CrossPlatformInputManager.GetAxis("FireGun"), 0);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile"))
        {
            hitpoints -= 1;
            
            if (hitpoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
