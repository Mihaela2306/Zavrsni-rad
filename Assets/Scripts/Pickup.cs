using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isCoin, isHeal;
    private bool isCollected;

    public GameObject pickupEffect;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // Logic for the player picking up a pickup
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isCollected) {
            // Logic for picking up a coin collectable
            if (isCoin) {
                LevelManager.instance.coinsCollected++;
                isCollected = true;
                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, transform.rotation);
                UIController.instance.UpdateCoinCount();
            }
            
            // Logic for picking up a health pickup
            if (isHeal) {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth) {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                }
            }
        }
    }
}
