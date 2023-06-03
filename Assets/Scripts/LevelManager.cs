using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int coinsCollected;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function for starting the co routine
    public void RespawnPlayer() {
        StartCoroutine(RespawnCo());
    }

    // Co routine for waiting a certain amount of time before respawning the player
    // And for reseting the position and the amount of health for the player
    private IEnumerator RespawnCo() {
        PlayerController.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(8);
        
        yield return new WaitForSeconds(waitToRespawn);

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }
}
