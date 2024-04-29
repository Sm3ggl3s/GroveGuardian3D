using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool GameIsOver = false;
    public static bool GardenIsDestroyed = false;

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(GameIsOver) {
            return;
        }

        /* Shortcut to end game */
        if(Input.GetKeyDown("e")) {
            EndGame();
        }

        // if Greenhouse is destroyed, end the game
        if(GardenIsDestroyed) {
            EndGame();
        }
    }

    void EndGame() {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
