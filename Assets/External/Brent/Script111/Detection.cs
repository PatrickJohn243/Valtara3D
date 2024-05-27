using UnityEngine;

public class Detection : MonoBehaviour
{
    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees
    public float maxDistance = 10f; // Maximum distance for the raycast
    public bool drawRaycastInScene = true; // Toggle to show/hide raycast in the scene
    public Color raycastColor = Color.red; // Default color of the raycast
    public Color hitColor = Color.green; // Color when the raycast hits an object

    public LayerMask raycastLayerMask; // Layer mask for the raycast

    private float currentRotationAngle; // Current rotation angle of the object
    private bool rotatingRight = true; // Flag to determine rotation direction
    

    public Transform player;
    public float playerDistanceThreshold = 5f;

    [HideInInspector]
    public bool playerDetected { get; private set; }
    public float distanceToPlayer { get; private set; }
    public bool hasHitObject = false;

    private void Update()
    {
        RotateObject();
        CastRaycast();
    }

    void RotateObject()
    {
        // Rotate the object back and forth within the specified range
        if (!hasHitObject)
        {
            if (rotatingRight)
            {
                currentRotationAngle += Time.deltaTime * 45f; // Adjust rotation speed as needed
                if (currentRotationAngle >= maxRotationAngle)
                {
                    rotatingRight = false;
                }
            }
            else
            {
                currentRotationAngle -= Time.deltaTime * 45f; // Adjust rotation speed as needed
                if (currentRotationAngle <= -maxRotationAngle)
                {
                    rotatingRight = true;
                }
            }

            Quaternion parentRotation = transform.parent.rotation;
            transform.rotation = parentRotation * Quaternion.Euler(0f, currentRotationAngle, 0f);
        }

        Vector3 currentPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosXZ = new Vector3(player.position.x, 0, player.position.z);
        distanceToPlayer = Vector3.Distance(currentPosXZ, playerPosXZ);
        //print(distanceToPlayer);

        if (distanceToPlayer > playerDistanceThreshold)
        {
            hasHitObject = false; // Reset the hit flag if the player is far enough
        }

    }

    void CastRaycast()
    {
        if (!hasHitObject)
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance, raycastLayerMask   ))
            {
                // Raycast hit an object
                if (drawRaycastInScene)
                    Debug.DrawRay(transform.position, direction * hit.distance, hitColor);

                hasHitObject = true;
                playerDetected = true;
            }
            else
            {
                // Raycast did not hit any object
                if (drawRaycastInScene)
                    Debug.DrawRay(transform.position, transform.forward * maxDistance, raycastColor);
            }
        }
    }

    public void SetPlayerDetected(bool detected)
    {
        playerDetected = detected;
    }
}