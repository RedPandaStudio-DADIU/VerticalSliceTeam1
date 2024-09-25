using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;  // Jump force for player
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundDistance = 0.7f;
    [SerializeField] private LayerMask groundMask; 
    
    private float freeRockRange = 15f;
    [SerializeField] private LayerMask roadMask; 


    private bool isGrounded;
    private bool canDoubleJump = false; 

    private Vector3 moveDirection;
    private Rigidbody playerRigidbody;
    [SerializeField] private Transform cameraTransform; // Reference to the camera's transform
    
    private CirclesManager circlesManager;  // Reference to the CirclesManager
    private RockManager rockManager;  // Reference to the RockManager
    private int currentCircleIndex = -1;    // Store the current circle index
    private int currentRockIndex = -1;      // Store the current rock index
    private int currentRockSetIndex = -1;   // Store the current rock set index
    private int currentFreeRockIndex = -1;  // Store the index for the free rock

    private bool isCarryingRock = false;    // Track if the player is carrying a rock
    private GameObject currentRock;         // The rock the player is carrying

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        circlesManager = FindObjectOfType<CirclesManager>();
        rockManager = FindObjectOfType<RockManager>();
        groundDistance = 0.7f;

    
    }

    private void Update()
    {
        ProcessInputs();
        MovePlayer();

        if(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, roadMask)){
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        Debug.Log("isGrounded check "+isGrounded);


       if (isGrounded){
            canDoubleJump = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(); 
            }
            else if (canDoubleJump)
            {
                Jump(); 
                canDoubleJump = false; // Reset double jump
            }
        }

        // Handle picking up and placing rocks
        if (isCarryingRock && Input.GetKeyDown(KeyCode.E))
        {
            if (currentFreeRockIndex != -1)
            {
                PlaceFreeRock();  
            }
            else
            {
                PlaceRock();  
            }
        }
        else if (!isCarryingRock && currentRockIndex != -1 && Input.GetKeyDown(KeyCode.E))
        {
            PickupRock();
        }
        else if (!isCarryingRock && currentFreeRockIndex != -1 && Input.GetKeyDown(KeyCode.E))
        {
            PickupFreeRock();
        }

        // Disable all circle interaction if carrying a rock
        if (isCarryingRock)
        {
            circlesManager.SetCircleInteraction(false);
        }
        else
        {
            circlesManager.SetCircleInteraction(true);
        }

        if (currentCircleIndex != -1 && Input.GetKeyDown(KeyCode.E))
        {
            circlesManager.RemoveObstacle(currentCircleIndex); // Remove the obstacle associated with the current circle
            Debug.Log("Pressed R, removing obstacle for circle: " + currentCircleIndex);
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

        // Move the rock behind the player if it's being carried
        if (isCarryingRock && currentRock != null)
        {
            currentRock.transform.position = transform.position + transform.right * -2.5f + transform.forward * -2f; // Behind and to the side
        }

    }

    private void Jump()
    {
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        Debug.Log("Jump");
    }

    private void PickupRock()
    {
        if (rockManager != null)
        {
            currentRock = rockManager.GetRock(currentRockIndex);
            Debug.Log("Current pick rock index: " + currentRockIndex);
           
            if (currentRock != null && !isCarryingRock)
            {
                isCarryingRock = true;
                currentRock.GetComponent<Rigidbody>().isKinematic = true; // Make rock float
                Debug.Log("Picked up the rock: " + currentRock.name);
            }
        }
    }

    private void PlaceRock()
        {
            //GameObject rockSet = rockManager.GetRockSet(currentRockIndex);
            GameObject rockSet = rockManager.GetRockSet(currentRockSetIndex);
          
            Debug.Log("Current rock index: " + currentRockIndex);
            Debug.Log("Attempting to place rock with index: " + currentRockIndex);

            Debug.Log("Rock set found: " + (rockSet != null ? rockSet.name : "null"));

            
            if (rockManager != null)
            {
                //GameObject rockSet = rockManager.GetRockSet(currentRockIndex);
                if (rockSet != null && rockManager.IsMatchingRockAndSet(currentRockIndex, currentRockSetIndex))
                {
                    currentRock.transform.position = rockSet.transform.position; // Place rock on rock set
                    currentRock.GetComponent<Rigidbody>().isKinematic = false;  // Disable floating
                    isCarryingRock = false;
                    currentRock = null;
                    currentRockIndex = -1; // Reset the rock index after placing the rock
                    Debug.Log("Placed rock on: " + rockSet.name);
                    currentRockSetIndex = -1; // Reset the rock set index after placing the rock
                }
                else
                {
                    Debug.Log("Rock and RockSet do not match.");
                }
            }
        }
    
    private void PickupFreeRock()
    {
        if (rockManager != null)
        {
            currentRock = rockManager.GetFreeRock(currentFreeRockIndex);
            if (currentRock != null && !isCarryingRock)
            {
                isCarryingRock = true;
                currentRock.GetComponent<Rigidbody>().isKinematic = true;
                 Debug.Log("Picked up the free rock: " + currentRock.name);
      
            }
        }
    }

    private void PlaceFreeRock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, freeRockRange, groundMask))
        {
            currentRock.transform.position = hit.point;  
            currentRock.GetComponent<Rigidbody>().isKinematic = false;
            isCarryingRock = false;
            currentRock = null;
            currentFreeRockIndex = -1;
        }
    }

    // Detect when the player enters the circle
    private void OnTriggerEnter(Collider other)
{
    // Check if the player entered a circle
    if (other.CompareTag("Circle") && circlesManager != null)
    {
        int circleIndex = circlesManager.GetCircleIndex(other.gameObject);
        if (circleIndex != -1 && !isCarryingRock)
        {
            currentCircleIndex = circleIndex; // Store the current circle index
            Debug.Log("Player entered circle: " + other.gameObject.name);
        }
    }
    
    // Check if the player is near a rock
    if (other.CompareTag("Rock") && rockManager != null)
    {
        int rockIndex = rockManager.GetRockIndex(other.gameObject);
        if (rockIndex != -1)
        {
            currentRockIndex = rockIndex; // Store the current rock index
            Debug.Log("Player near rock: " + other.gameObject.name);
        }
    }
    
    // Check if the player is near a rock set
    if (other.CompareTag("RockSet") && isCarryingRock)
    {
        int rockSetIndex = rockManager.GetRockSetIndex(other.gameObject);
            if (rockSetIndex != -1)
            {
                currentRockSetIndex = rockSetIndex; // Store the rock set index
                Debug.Log("Player near rock set: " + other.gameObject.name);
            }
    }

    if (other.CompareTag("FreeRock") && rockManager != null)
    {
         int freeRockIndex = rockManager.GetFreeRockIndex(other.gameObject);
            if (freeRockIndex != -1)
            {
                currentFreeRockIndex = freeRockIndex;
            }
    }
}

private void OnTriggerExit(Collider other)
{
    // Handle when the player exits a circle
    if (other.CompareTag("Circle") && circlesManager != null)
    {
        int circleIndex = circlesManager.GetCircleIndex(other.gameObject);
        if (circleIndex != -1 && !isCarryingRock)
        {
            currentCircleIndex = -1; // Reset the circle index
            Debug.Log("Player exited circle: " + other.gameObject.name);
        }
    }

    // Handle when the player exits a rock area
    if (other.CompareTag("Rock") && rockManager != null)
    {
        int rockIndex = rockManager.GetRockIndex(other.gameObject);
        if (rockIndex != -1)
        {
            //currentRockIndex = -1; // Reset the rock index
            Debug.Log("Player left rock area: " + other.gameObject.name);
        }
    }
    
    // Handle when the player exits a rock set area
    if (other.CompareTag("RockSet") && isCarryingRock== false)
    {
        currentRockSetIndex = -1; // Reset the rock set index
        currentRockIndex = -1;  
        Debug.Log("Player exited rock set area: " + other.gameObject.name);
    }
}

}