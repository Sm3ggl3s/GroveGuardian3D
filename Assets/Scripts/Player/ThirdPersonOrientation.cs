using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonOrientation : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate orientation
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        // Rotate player object
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientation.forward * vertical + orientation.right * horizontal;

        if (inputDirection != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.fixedDeltaTime * rotationSpeed);
        }
    }
}