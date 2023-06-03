using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public GameObject deathEffect, collectible;
    [Range(0, 100)]public float chanceToDrop;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // If the player hits the enemy deactivate that enemy and show death effect
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            PlayerController.instance.Bounce();

            // Chance of dropping a collectible and instantiating it
            float dropSelect = Random.Range(0f, 100f);
            if (dropSelect <= chanceToDrop) {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }
        }
    }
}
