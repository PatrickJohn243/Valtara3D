using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float jumpForce = 5f; // Force applied for jumping
    public float minJumpHeight = 2f; // Height for the jump
    public float maxJumpHeight = 4f; // Height for the jump
    public float jumpCooldown = 1f; // Cooldown time between jumps
    private float nextJumpTime = 0f; // Initialize nextJumpTime to 0

    public float roamingRadius = 5f; // Radius within which the slime can roam
    private Vector3 initialPosition;

    private Detection detection;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.parent.position;
        detection = GetComponentInChildren<Detection>();
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        // Check if it's time to jump
        if (Time.time > nextJumpTime)
        {
            Vector3 jumpDirection;
            if (detection.playerDetected && detection.hasHitObject)
            {
                // Jump towards the player
                jumpDirection = (detection.player.position - transform.position).normalized;
                jumpDirection = new Vector3(jumpDirection.x, (int) Random.Range(minJumpHeight, maxJumpHeight), jumpDirection.z);
            }
            else
            {
                Vector3 currentPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 initialPosXZ = new Vector3(initialPosition.x, 0, initialPosition.z);
                float distanceFromInitialPosition = Vector3.Distance(currentPosXZ, initialPosXZ);

                // If within the roaming radius, jump in a random direction
                if (distanceFromInitialPosition < roamingRadius)
                {
                    Vector3 randomHorizontalDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                    jumpDirection = new Vector3(randomHorizontalDirection.x, (int) Random.Range(minJumpHeight, maxJumpHeight), randomHorizontalDirection.z);
                }
                // If outside the roaming radius, jump towards the initial position
                else
                {
                    detection.SetPlayerDetected(false);
                    jumpDirection = (initialPosition - transform.position).normalized;
                    jumpDirection = new Vector3(jumpDirection.x, maxJumpHeight, jumpDirection.z);
                }
            }
            

            Vector3 lookDirection = new Vector3(jumpDirection.x, 0f, jumpDirection.z);
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            // Apply the jump force
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            // Reset the next jump time
            nextJumpTime = Time.time + jumpCooldown;
            
        }
    }
}