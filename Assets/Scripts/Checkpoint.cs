using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite cpOn, cpOff;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function in Unity for triggering an event
    // Deactivates checkpoints, turns on the newest checkpoint and sets the spawn point for the player
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            CheckpointController.instance.DeactivateCheckpoints();
            theSR.sprite = cpOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    // Function for turning the checkpoint off when another checkpoint is active
    public void ResetCheckpoint() {
        theSR.sprite = cpOff;
    }
}
