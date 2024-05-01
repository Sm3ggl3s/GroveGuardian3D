using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenDamage : MonoBehaviour {
    public float health = 100f;
    
    // Method to deal damage to Garden based on what enemy has reached it
    public void takeDamage(float damageAmount) {
        // If the next thing of damage is enough to destroy the Garden, then destroy the Garden
        if(health - damageAmount <= 0) {
            health = 0;
            //Debug.Log(health);
            GameManager.GardenIsDestroyed = true;
            Destroy(gameObject);
            return;
        }

        // Else, damage the Garden
        health -= damageAmount;
        //Debug.Log(health);
    }
}