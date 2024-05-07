using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    private Rigidbody playerBullet_rb;
    public int damage = 10;

    private Transform target;
    public string targetTag = "Enemy";

    private void Awake() {
        playerBullet_rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 30f;
        playerBullet_rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            EnemyBasic enemy = other.GetComponent<EnemyBasic>();
            Debug.Log("Enemy Found" + enemy);
            if(enemy != null){
                enemy.TakeDamage(damage, 0);
                Debug.Log("Enemy Hit");
            }
        }
        Destroy(gameObject);

    }
}
