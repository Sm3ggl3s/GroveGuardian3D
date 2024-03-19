using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject heldItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key is pressed!");
            Vector3 newPosition = transform.position;
            newPosition.y += .3f;
            GameObject buildTurret = (GameObject)Instantiate(heldItem, newPosition, transform.rotation);
        }
    }
}
