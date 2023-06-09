using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;

    public SpriteRenderer theSR;

    public float distanceToAttackPlayer, chaseSpeed, waitAfterAttack;
    private Vector3 attackTarget;
    private float attackCounter;

    // Start is called before the first frame update
    void Start() {
        foreach(Transform point in points) {
            point.parent = null;
        }
    }

    // Update is called once per frame
    void Update() {
        if (attackCounter > 0) {
            attackCounter -= Time.deltaTime;
        } else {
            // If the player isn't close enough for the enemy to attack him
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer) {
                attackTarget = Vector3.zero;

                // Logic for moving the flying enemy
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f) {
                    currentPoint++;
                    if (currentPoint >= points.Length) {
                        currentPoint = 0;
                    }
                }

                if (transform.position.x < points[currentPoint].position.x) {
                    theSR.flipX = true;
                } else if (transform.position.x > points[currentPoint].position.x) {
                    theSR.flipX = false;
                }
            // If the player is close enough for the enemy to attack him
            } else {
                if (attackTarget == Vector3.zero) {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= .1f) {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }
}
