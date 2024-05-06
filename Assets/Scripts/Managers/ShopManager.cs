using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour {

    public static ShopManager _instance;

    public TextMeshProUGUI coinsText;
    public List<string> names = new List<string> { "Coccanut", "Fervor Flower", "Banana Burst", "Shackle Vine", "Blast Blossom", "Grove Grow"};
    public List<int> costs = new List<int> { 3, 4, 3, 4, 5, 3};
    
    [Header("Pack Data")]
    public List<string> packNames = new List<string> {"e", "e", "e"};
    public List<int> packCosts = new List<int> {0, 1, 2};

    public List<Button> packButtons = new List<Button>();
    

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log(packCosts.Count);
        coinsText.text = InventoryManager.instance.coins.ToString();
        GenerateNewPack(1);    
        GenerateNewPack(2);
        GenerateNewPack(3);
        
    }

    private void Update() {
        for (int i = 0; i < packCosts.Count; i++) {
            if (InventoryManager.instance.coins < packCosts[i]) {
                packButtons[i].interactable = false;
            } else {
                packButtons[i].interactable = true;
            }
        }
    }

    public void addTowerToInventory(int packNumber) {
        string towerName = packNames[packNumber - 1];
        for (int i = 0; i < names.Count; i++) {
            if (names[i] == towerName) {
                if (InventoryManager.instance.coins > costs[i]) {
                    InventoryManager.instance.removeCoins(costs[i]);
                    InventoryManager.instance.inventoryQuantities[i]++;
                    InventoryManager.instance.inventoryQuantitiesText[i].text = InventoryManager.instance.inventoryQuantities[i].ToString();
                    break;
                }
            }
        }
    }

    // Function to generate a new pack
    void GenerateNewPack(int packNumber)
    {
        int randomIndex = Random.Range(0, names.Count); // Generate a random index
        string packName = names[randomIndex];
        int packCost = costs[randomIndex];

        // Assign the pack name and cost to the corresponding lists
        switch(packNumber)
        {
            case 1:
                packNames[0] = packName;
                packCosts[0] = packCost;
                break;
            case 2:
                packNames[1] = packName;
                packCosts[1] = packCost;
                break;
            case 3:
                packNames[2] = packName;
                packCosts[2] = packCost;
                break;
            default:
                Debug.LogError("Invalid pack number");
                break;
        }
    }

    // Function to generate a new pack for Pack 1
    public void GenerateNewPack1()
    {
        GenerateNewPack(1);
    }

    // Function to generate a new pack for Pack 2
    public void GenerateNewPack2()
    {
        GenerateNewPack(2);
    }

    // Function to generate a new pack for Pack 3
    public void GenerateNewPack3()
    {
        GenerateNewPack(3);
    }

}
