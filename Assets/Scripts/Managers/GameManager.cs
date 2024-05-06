using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool GameIsOver = false;
    public static bool GardenIsDestroyed = false;
    public static bool WavesDone = false;

    // Build indices:
    // Level1: 0
    // Level2: 1
    public static int LEVEL_ONE_SCENE_INDEX = 0;
    public static int LEVEL_TWO_SCENE_INDEX = 1;

    public GameObject gameOverUI;
    public GameObject youWinUI;

    // Start is called before the first frame update
    void Start() {
        GameIsOver = false;
        GardenIsDestroyed = false;
        WavesDone = false;
    }

    // Update is called once per frame
    void Update() {
        if(GameIsOver) {
            return;
        }

        /* Shortcut to end game */
        /*
        if(Input.GetKeyDown("e")) {
            WinGame();
        }
        */

        // if Greenhouse is destroyed, end the game
        if(GardenIsDestroyed) {
            LoseGame();
        }

        // if Greenhouse survives all waves and there are no more enemies on the map
        if(WavesDone && GameObject.Find("RobotEnemy1(Clone)") == null) {
            WinGame();
        }
    }

    // if game is lost, toggle active state of GameOverUI
    void LoseGame() {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    // if game is won, toggle active state of GameWonUI
    void WinGame() {
        GameIsOver = true;
        youWinUI.SetActive(true);
    }
}
