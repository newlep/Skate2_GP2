using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithCollision : MonoBehaviour
{
    public Transform target;               // The skateboard (player) transform
    public Vector3 offset = new Vector3(0, 2, -5); // Default offset position from the target
    public float smoothSpeed = 0.125f;     // Smoothness of the camera movement
    public float collisionRadius = 0.5f;  // Radius for collision detection
    public LayerMask collisionLayers;     // Layers that the camera will consider for collisions

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = offset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired camera position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Adjust position to prevent clipping
        Vector3 adjustedPosition = HandleCollisions(target.position, desiredPosition);

        // Smoothly interpolate to the adjusted position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, adjustedPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera to always look at the target
        transform.LookAt(target);
    }

    Vector3 HandleCollisions(Vector3 targetPosition, Vector3 desiredPosition)
    {
        Vector3 direction = desiredPosition - targetPosition;
        float distance = direction.magnitude;
        Ray ray = new Ray(targetPosition, direction.normalized);

        if (Physics.SphereCast(ray, collisionRadius, out RaycastHit hit, distance, collisionLayers))
        {
            // Move the camera to the hit point, slightly offset to avoid full clipping
            return hit.point - direction.normalized * collisionRadius;
        }

        return desiredPosition;
    }
}
