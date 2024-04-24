using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonOrientation : MonoBehaviour {
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject combatCamera;
    public GameObject basicCamera;

    public CameraStyle currentCameraStyle;

    public enum CameraStyle {
        Basic,
        Combat
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

        // Camra Style Switching
        if (Input.GetMouseButtonDown(1)) {
            SwitchCameraStyle(CameraStyle.Combat);
        } else if (Input.GetMouseButtonUp(1)) {
            SwitchCameraStyle(CameraStyle.Basic);
        }

        // Rotate Orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotate Player Object
        if (currentCameraStyle == CameraStyle.Basic) {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero) {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        } else if (currentCameraStyle == CameraStyle.Combat) {
            Vector3 dirToCombatLookAt = player.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;
            
            playerObj.forward = dirToCombatLookAt.normalized;
            playerObj.rotation = Quaternion.Euler(0, playerObj.rotation.eulerAngles.y, 0);
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle) {
        combatCamera.SetActive(false);
        basicCamera.SetActive(false);

        if (newStyle == CameraStyle.Basic) {
            basicCamera.SetActive(true);
        } 
        if (newStyle == CameraStyle.Combat) {
            combatCamera.SetActive(true);
        }

        currentCameraStyle = newStyle;
    }

}