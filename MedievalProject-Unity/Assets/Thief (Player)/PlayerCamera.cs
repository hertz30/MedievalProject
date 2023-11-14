using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // get the transform position of Igor. ~TW
    public Vector3 offset = new Vector3(0f, 2f, 5f);  // offset the camera from the player. ~TW (changed offset.z to positive)
    public float rotationSpeed = 5f;  // how fast camera will rotate. ~TW
    public float maxDistance = 10f;   // how far the camera can be from Igor. ~TW

    void LateUpdate() // LateUpdate is IMPORTANT for smooth "delayed" camera scrolling. ~TW
    {
        if (player != null)
        {
            // Camera should sit behind Igor, positive forward to offset looking behind. ~TW (changed -player.forward to player.forward)
            Vector3 desiredPosition = player.position + player.forward * offset.z + player.up * offset.y;

            // Lerp & Time.deltaTime are used to smoothen the camera when FOV is moving, w/ rotation speed. ~TW
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Raycast is used to help prevent camera from clipping through objects. ~TW
            RaycastHit hit;
            if (Physics.Raycast(player.position, smoothedPosition - player.position, out hit, maxDistance))
            {
                // If camera hits an object, set the pos to the exact collision location of camera & obj. ~TW
                transform.position = hit.point;
            }
            else
            {
                // no collisions ~TW
                transform.position = smoothedPosition;
            }

            // always focus camera on player. ~TW
            transform.LookAt(player);
        }
    }
}
