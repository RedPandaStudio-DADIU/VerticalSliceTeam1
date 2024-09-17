using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player and Camera Settings")]
    [SerializeField] private Transform playerBody; // The player object the camera will follow
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -5); // Offset from player (adjust as needed)
    [SerializeField] private float mouseSensitivity = 100f; // Sensitivity for mouse movement
    [SerializeField] private float smoothSpeed = 10f; // Camera smooth follow speed

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        // Lock the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCameraWithMouse();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Camera follows the player's position, but the camera rotates independently
        Vector3 desiredPosition = playerBody.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void RotateCameraWithMouse()
    {
        // Get mouse input for X (horizontal) and Y (vertical) axes
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (Y axis, looking up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent looking too far up or down

        // Adjust horizontal rotation (X axis, looking left/right)
        yRotation -= mouseX;
        // No need to clamp yRotation as we want to rotate freely around

        // Apply the rotations to the camera itself (independent of player)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}