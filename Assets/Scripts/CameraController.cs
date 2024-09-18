using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform player;        // Player to follow
    public float distance = 5.0f;   // Distance from the player
    public float height = 3.0f;     // Height above the player
    public float cameraSpeed = 10.0f; // Speed at which the camera rotates

    private float currentX = 0.0f;  // Rotation around Y axis (horizontal)
    private float currentY = 0.0f;  // Rotation around X axis (vertical)
    public float sensitivityX = 4.0f; // Sensitivity for horizontal rotation
    public float sensitivityY = 2.0f; // Sensitivity for vertical rotation
    public float minYAngle = -40f;  // Limit on how far the camera can look down
    public float maxYAngle = 60f;   // Limit on how far the camera can look up

    private void LateUpdate()
    {
        // Get mouse movement for rotating the camera
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle); // Limit vertical angle

        // Calculate the desired camera position and rotation
        Vector3 direction = new Vector3(0, height, -distance); // Offset behind player
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0); // Rotation by mouse
        transform.position = player.position + rotation * direction; // Position camera relative to player
        transform.LookAt(player.position); // Camera always looks at player
    }
}