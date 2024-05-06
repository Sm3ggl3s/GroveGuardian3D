using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public List<string> names = new List<string> { "Coccanut", "Fervor Flower", "Banana Burst", "Shackle Vine", "Blast Blossom", "Grove Grow"};
    public List<int> costs = new List<int> { 3, 4, 3, 4, 5, 3};
    
    public List<string> packNames = new List<string> {"e", "e", "e"};
    public List<int> packCosts = new List<int> {0, 1, 2};

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(packCosts.Count);
        GenerateNewPack(1);
        GenerateNewPack(2);
        GenerateNewPack(3);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
