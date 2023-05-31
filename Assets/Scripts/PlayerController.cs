using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce;
    public Rigidbody2D theRB;
    private SpriteRenderer theSR;

    private bool isGrounded, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
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

        // Controlling the animations on the player
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }
}
