using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        checkpoints = FindObjectsOfType<Checkpoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Deactivate all checkpoints in the scene
    public void DeactivateCheckpoints() {
        foreach (Checkpoint checkpoint in checkpoints) {
            checkpoint.ResetCheckpoint();
        }
    }

    // When the player hits a checkpoint set a spawn point for that checkpoint
    // In params new spawn point
    public void SetSpawnPoint(Vector3 newSpawnPoint) {
        spawnPoint = newSpawnPoint;
    }
}
