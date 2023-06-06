using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start() {
        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update() {
        // Moving the bullet in the right direction
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    // Function in Unity for triggering an event
    // Function for dealing damage with the bullet and destroying the bullet
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerHealthController.instance.DealDamage();
        }

        AudioManager.instance.PlaySFX(1);
        Destroy(gameObject);
    }
}
