using UnityEngine;

public class PlayerController : MonoBehaviour
{
     [SerializeField] private float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveSpeed;
            playerRigidbody.velocity = new Vector3(moveVelocity.x, playerRigidbody.velocity.y, moveVelocity.z);
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0); // Stop movement when no input
        }
    }
}