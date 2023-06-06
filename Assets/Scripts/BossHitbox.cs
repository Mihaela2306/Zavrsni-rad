using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public BossController bossCont;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // If the player is above the boss and hits him boss takes a hit
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && PlayerController.instance.transform.position.y > transform.position.y) {
            bossCont.TakeHit();
            PlayerController.instance.Bounce();
            gameObject.SetActive(false);
        }
    }
}
