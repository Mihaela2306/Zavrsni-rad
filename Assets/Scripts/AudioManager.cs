using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;
    public AudioSource backgroundMusic, levelEndMusic;

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

    // Function for playing a specific sound effect
    public void PlaySFX(int soundToPlay) {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);

        soundEffects[soundToPlay].Play();
    }

    // Function for playing level victory
    public void PlayLevelVictory() {
        backgroundMusic.Stop();
        levelEndMusic.Play();
    }
}
