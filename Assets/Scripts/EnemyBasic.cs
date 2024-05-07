using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, float slowEffect){
        health -= damage;
        if(health <= 0){
            Die();
        }
        EnemyMovement a = transform.GetComponent<EnemyMovement>();
        if(a.slowed < slowEffect || a.slowed == 1)
            a.slowed = slowEffect;
            StartCoroutine(a.ReduceSpeed(1f));
    }

    void Die(){
        Destroy(gameObject);
        InventoryManager.instance.addCoins(1);
        Debug.Log("Enemy Died. Coins added Total is: " + InventoryManager.instance.coins);
    }
}
