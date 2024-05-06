using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    private int coins = 5;
    public TextMeshProUGUI coinText;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure canvas is initially hidden
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle canvas visibility when E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCanvas();
        }

        // Hide canvas when Esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCanvas();
        }
        coinText.text = "COINS : " + coins;
    }

    // Function to toggle canvas visibility
    void ToggleCanvas()
    {
        canvas.enabled = !canvas.enabled;
    }

    // Function to hide the canvas
    void HideCanvas()
    {
        canvas.enabled = false;
    }
}
