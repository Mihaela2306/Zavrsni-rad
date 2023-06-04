using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // Function for ending a level when the player passes the flag pole
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            LevelManager.instance.EndLevel();
        }
    }
}
