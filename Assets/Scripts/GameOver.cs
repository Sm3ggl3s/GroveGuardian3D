using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    void onEnable() {

    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restarts currently-loaded scene
    }

    public void Menu() {
        Debug.Log("Go to menu..");
    }
}
