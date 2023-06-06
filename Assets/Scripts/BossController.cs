using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitbox;

    [Header("Health")]
    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;

    // Start is called before the first frame update
    void Start() {
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update() {
        switch (currentState) {
            // Logic for boss shooting
            case bossStates.shooting :
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0) {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;
            // Logic fot boss getting hurt
            case bossStates.hurt :
                if (hurtCounter > 0) {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter <= 0) {
                        currentState = bossStates.moving;
                        mineCounter = timeBetweenMines;
                        if (isDefeated) {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.SetActive(true);
                            AudioManager.instance.StopBossMusic();
                            currentState = bossStates.ended;
                        }
                    }
                }

                break;
            // Logic for boss moving
            case bossStates.moving :
                if (moveRight) {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x) {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;
                        EndMovement();
                    }
                } else {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x) {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0) {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }
    }

    // Function for boss taking a hit
    public void TakeHit() {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");
        AudioManager.instance.PlaySFX(0);

        // Removing all the mines before placing new ones
        BossMine[] mines = FindObjectsOfType<BossMine>();
        if (mines.Length > 0) {
            foreach (BossMine mine in mines) {
                mine.Explode();
            }
        }

        health--;
        if (health <= 0) {
            isDefeated = true;
        } else {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    } 

    // Function for ending movement
    private void EndMovement() {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitbox.SetActive(true);
    }
}
