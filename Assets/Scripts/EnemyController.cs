using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool movingRight;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        theRB = GetComponent<Rigidbody2D>();
        leftPoint.parent = null;
        rightPoint.parent = null;
        moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // Moving the enemy for a certain period of time
        if (moveCount > 0) {
            moveCount -= Time.deltaTime;
            
            // Moving right
            if (movingRight) {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                theSR.flipX = true;
                if (transform.position.x > rightPoint.position.x){
                    movingRight = false;
                }
            // Moving left
            } else {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                theSR.flipX = false;
                if (transform.position.x < leftPoint.position.x) {
                    movingRight = true;
                }
            }

            // If the time for moving has passed set up the time for waiting
            if (moveCount <= 0) {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);
        // Enemy waiting and standing still for a certain period of time
        } else if (waitCount > 0) {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            // If the time for waiting has passed set up the time for moving
            if (waitCount <= 0) {
                moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
            }
            
            anim.SetBool("isMoving", false);
        }
    }
}
