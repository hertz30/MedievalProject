using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 3.5f, -10f);
    
    [Header("Smoothness Settings")]
    public float followSpeed = 5f;       // Speed for normal movement
    public float collisionSpeed = 8f;   // Speed for zooming in/out of walls
    
    public float maxDistance = 10f;
    public LayerMask collisionLayers;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + player.forward * offset.z + player.up * offset.y + player.right * offset.x;
            RaycastHit hit;
            Vector3 direction = desiredPosition - player.position;
            float targetDistance = direction.magnitude;

            Vector3 finalTargetPos = desiredPosition;

            if (Physics.Raycast(player.position, direction.normalized, out hit, targetDistance, collisionLayers))
            {
                finalTargetPos = hit.point + hit.normal * 0.2f;
            }

            transform.position = Vector3.Lerp(transform.position, finalTargetPos, collisionSpeed * Time.deltaTime);

            // // LEAVE THIS HERE BUT COMMENTED PLEASE: ~Tulsano
            // // Raycast is used to help prevent camera from clipping through objects. ~TW
            // RaycastHit hit;
            // if (Physics.Raycast(player.position, smoothedPosition - player.position, out hit, maxDistance))
            // {
            //     // If camera hits an object, set the pos to the exact collision location of camera & obj. ~TW
            //     transform.position = hit.point;
            // }
            // else
            // {
            //     // no collisions ~TW
            //     transform.position = smoothedPosition;
            // }

            Vector3 lookAtTarget = player.position + player.up * 2f;
            transform.LookAt(lookAtTarget);
        }
    }
}