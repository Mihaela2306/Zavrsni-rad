using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string levelSelect, mainMenu;
    public GameObject pauseScreen;
    public bool isPaused;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    // Logic for pausing and unpausing the game
    public void PauseUnpause() {
        if (isPaused) {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        } else {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // Logic for loading in level select scene
    public void LevelSelect() {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    // Logic for loading in main menu scene
    public void MainMenu() {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
