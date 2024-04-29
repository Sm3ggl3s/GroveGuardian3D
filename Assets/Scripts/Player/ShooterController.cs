using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterController : MonoBehaviour {

    [Header("Camera Settings")]
    [SerializeField] private CinemachineFreeLook combatCamera;

    [Header("Aim Settings")]
    [SerializeField] private LayerMask aimColliderLayerMask;
    private Ray ray;
    
    [Header("Bullet Settings")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private Vector3 mouseWorldPosition;

    [Header("Timer Settings")]
    private float timeSinceLastShot = 0f;
    private float shootInterval = 0.5f;

    [Header("Animation")]
    public Transform animatorObject;
    private Animator animator;


    private void Start() {
        aimColliderLayerMask = LayerMask.GetMask("Default", "WhatIsGround");

        // Animation
        animator = animatorObject.GetComponent<Animator>();
    }

    private void Update() {
        if (CinemachineCore.Instance.IsLive(combatCamera)) {
            animator.SetBool("IsShooting", true);
            Aim();
            timeSinceLastShot += Time.deltaTime;
            if (Input.GetMouseButton(0) && timeSinceLastShot >= shootInterval) {
                Shoot();
                timeSinceLastShot = 0f;
            }
        } else {
            animator.SetBool("IsShooting", false);
        }
        
    }

    private void Aim() {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            mouseWorldPosition = raycastHit.point;
        }
    }

    private void Shoot() {
        // Shoot the bullet
        Vector3 aimDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
    }
}
