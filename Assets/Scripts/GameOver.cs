using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    void onEnable() {

    }

    public void Retry() {
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restarts currently-loaded scene
    }

    public void Menu() {
        /*TO-DO: Use SceneManager to load menu scene when pressed*/
        Debug.Log("Go to menu..");
    }
}
