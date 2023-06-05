using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, jumpForce;
    public Rigidbody2D theRB;
    private SpriteRenderer theSR;

    private bool isGrounded, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Animator anim;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        // If the pause menu is not open do things as the player
        if (!PauseMenu.instance.isPaused && !stopInput) {
            // If the player didn't get hit the player can walk and jump
            if (knockBackCounter <= 0) {
                // Moving the player
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                // Checking if the player is touching the ground to know if the player can jump
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
                if (isGrounded) {
                    canDoubleJump = true;
                }

                // Player jumping
                if (Input.GetButtonDown("Jump")) {
                    if (isGrounded) {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
                    } else if (canDoubleJump) {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
                    }
                }

                // Controlling which way the player is turned
                if (theRB.velocity.x < 0) {
                    theSR.flipX = true;
                } else if (theRB.velocity.x > 0) {
                    theSR.flipX = false;
                }
            // If the player got hit than decrease the knockBackCounter and move him away from the danger
            } else {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX) {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                } else {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }
        }

        // Controlling the animations on the player
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    // KnockBack function initializes the counter for the knock back and stops the player from moving
    // it also starts the animation for player hurt
    public void KnockBack() {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
        anim.SetTrigger("hurt");
    }

    // Bounce function bounces the player after killing an enemy
    public void Bounce() {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }

    // Function in Unity for triggering an event on collision with another object
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = other.transform;
        }
    }

    // Function in Unity for triggering an event on exiting collision with another object
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = null;
        }
    }
}
