using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject theBossBattle;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // Function for starting the boss battle
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            theBossBattle.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayBossMusic();
        }
    }
}
