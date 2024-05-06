using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    private Rigidbody playerBullet_rb;

    private void Awake() {
        playerBullet_rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 30f;
        playerBullet_rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
