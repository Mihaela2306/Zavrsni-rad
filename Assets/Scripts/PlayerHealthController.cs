using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        // Controlling the invincibility of the player after he gets hit
        if (invincibleCounter > 0) {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0) {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    // Deal Damage is used for decreasing the players health
    public void DealDamage() {
        if (invincibleCounter <= 0) {
            currentHealth--;

            // If the player has no more health make it disappear
            if (currentHealth <= 0) {
                currentHealth = 0;
                gameObject.SetActive(false);
            // If the player got hit make it invincible for some time and call the knock back function
            } else {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }
}
