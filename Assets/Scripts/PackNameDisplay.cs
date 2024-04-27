using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackNameDisplay : MonoBehaviour
{
    public int packNumber;
    public ShopManager shopManager; // Reference to the ShopManager object
    public TextMeshProUGUI nameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shopManager != null && packNumber > 0)
        {
            nameText.text = shopManager.packNames[packNumber - 1];
        }
        else
        {
            Debug.LogWarning("ShopManager reference is not set in PackDisplay script.");
        }
    }
}
