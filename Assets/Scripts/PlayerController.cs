using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform; // Reference to the camera's transform
  
    private CirclesManager circlesManager;  // Reference to the CirclesManager
    private int currentCircleIndex = -1;    // Store the current circle index

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        circlesManager = FindObjectOfType<CirclesManager>();

    if (circlesManager == null)
    {
        Debug.LogError("CirclesManager not found in the scene!");
    }
    }

    private void Update()
    {
        ProcessInputs();
        MovePlayer();

        // If player is in a circle and presses "R"
        if (currentCircleIndex != -1 && Input.GetKeyDown(KeyCode.R))
        {
            circlesManager.RemoveObstacle(currentCircleIndex); // Remove the corresponding obstacle
        }
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        // Get the camera's forward and right vectors
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure forward and right vectors are aligned to the ground (Y-axis)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate the direction to move based on camera's orientation
        moveDirection = (forward * moveZ + right * moveX).normalized;
     }


    private void MovePlayer()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveSpeed;
            playerRigidbody.velocity = new Vector3(moveVelocity.x, playerRigidbody.velocity.y, moveVelocity.z);

            // Rotate player to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0); // Stop movement when no input
        }
    }


    // Detect when the player enters the circle
    private void OnTriggerEnter(Collider other)
    {
        if (circlesManager != null)
        {
            int circleIndex = circlesManager.GetCircleIndex(other.gameObject);
            if (circleIndex != -1)
            {
                currentCircleIndex = circleIndex; // Store the current circle index
                Debug.Log("Player entered circle: " + other.gameObject.name);
            }
        }
    }

    // Detect when the player exits the circle
    private void OnTriggerExit(Collider other)
    {
        int circleIndex = circlesManager.GetCircleIndex(other.gameObject);
            if (circleIndex != -1)
            {
                currentCircleIndex = -1; // Reset the circle index
                Debug.Log("Player exited circle: " + other.gameObject.name);
            }
        
    }
}