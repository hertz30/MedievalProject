using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // get the transform position of player. ~TW
    public Vector3 offset = new Vector3(0f, 3.5f, -10f);  // offset the camera from the player. ~TW (changed offset.z to positive)
    private float rotationSpeed = 100f;  // how fast camera will rotate. ~TW
    public float maxDistance = 10f;   // how far the camera can be from player. ~TW

    void LateUpdate() // LateUpdate is IMPORTANT for smooth "delayed" camera scrolling. ~TW
    {
        if (player != null)
        {
            // Camera should sit behind player, positive forward to offset looking behind. ~TW (changed -player.forward to player.forward)
            Vector3 desiredPosition = player.position + player.forward * offset.z + player.up * offset.y + player.right * offset.x;

            // Lerp & Time.deltaTime are used to smoothen the camera when FOV is moving, w/ rotation speed. ~TW
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // always focus camera on player. ~TW
            Vector3 lookAtTarget = player.position + player.up * 2f;  // camera will look towards head of player ~TW
            transform.LookAt(lookAtTarget);
        }
    }
}
            // LEAVE THIS HERE BUT COMMENTED PLEASE: ~Tulsano
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
