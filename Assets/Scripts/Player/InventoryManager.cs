using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance;


    public List<string> inventoryItems = new List<string> { "Cocannut", "FervorFlower", "BananaBurst", "ShackleVine", "BlastBloom", "Fertilizer"};
    public List<int> inventoryQuantities = new List<int> { 0, 0, 0, 0, 0, 0};

    public List<TextMeshProUGUI> inventoryQuantitiesText = new List<TextMeshProUGUI>();

    public int CoinTotal = 30; 

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        // Set all inventory quantities to 0
        for (int i = 0; i < inventoryQuantities.Count; i++) {
            inventoryQuantities[i] = 0;
        }
    }

    public void removeTowerFromInventory(string towerName) {
        for (int i = 0; i < inventoryItems.Count; i++) {
            if (inventoryItems[i] == towerName) {
                inventoryQuantities[i]--;
                inventoryQuantitiesText[i].text = inventoryQuantities[i].ToString();
            }
        }
    }

    public void addCoins(int amount) {
        CoinTotal += amount;
    }

    public void removeCoins(int amount) {
        CoinTotal -= amount;
    }


}
