using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager instance;

    [Header("GUI Elements")]
    public GameObject inventoryPanel;
    public GameObject shopPanel;

    [Header("Crosshair")]
    public GameObject crosshair;

    private bool isShopPanelActive = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        inventoryPanel.SetActive(true);
        shopPanel.SetActive(false);
        crosshair.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            ToggleShopPanel();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            CloseShopPanel();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    private void ToggleShopPanel() {
        isShopPanelActive = !isShopPanelActive;
        shopPanel.SetActive(isShopPanelActive);
        inventoryPanel.SetActive(!isShopPanelActive);
        crosshair.SetActive(false);
    }

    private void CloseShopPanel() {
        isShopPanelActive = false;
        shopPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        crosshair.SetActive(true);
    } 


}
