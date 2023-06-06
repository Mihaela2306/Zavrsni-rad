using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMine : MonoBehaviour
{
    public GameObject explosion;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // Function for the mine hurting the player and instantiating an explosion
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Explode();
            PlayerHealthController.instance.DealDamage();
        }
    }

    // Exploding the mine without player triggering it
    public void Explode() {
        AudioManager.instance.PlaySFX(3);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
