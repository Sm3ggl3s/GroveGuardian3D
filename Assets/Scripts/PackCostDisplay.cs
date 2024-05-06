using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackCostDisplay : MonoBehaviour
{
    public int packNumber;
    public TextMeshProUGUI costText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ShopManager._instance != null)
        {
            costText.text = "Cost : " + ShopManager._instance.packCosts[packNumber - 1];
        }
        else
        {
            Debug.LogWarning("ShopManager reference is not set in PackDisplay script.");
        }
    }
}
