using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartEmpty, heartHalf;

    public Text coinText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        // Setting the coin count at the start of the game to 0
        UpdateCoinCount();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update() {
        // Logic for fading the screen to black
        if (shouldFadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f) {
                shouldFadeToBlack = false;
            }
        }
        
        // Logic for fading the screen from black
        if (shouldFadeFromBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f) {
                shouldFadeFromBlack = false;
            }
        }
    }

    // Update Health Display updates how many hearts are displayed on the UI
    public void UpdateHealthDisplay() {
        switch (PlayerHealthController.instance.currentHealth) {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    // Update coin count updates how many coins the player has collected
    public void UpdateCoinCount() {
        coinText.text = LevelManager.instance.coinsCollected.ToString();
    }

    // Setting the bool values for fading to black
    public void FadeToBlack() {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    // Setting the bool values for fading from black
    public void FadeFromBlack() {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
