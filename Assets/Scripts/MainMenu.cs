using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Logic for start game button
    public void StartGame() {
        SceneManager.LoadScene(startScene);
    }

    // Logic for quit game button
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
