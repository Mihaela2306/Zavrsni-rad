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
                } else if (canDoubleJump) {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
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
}
