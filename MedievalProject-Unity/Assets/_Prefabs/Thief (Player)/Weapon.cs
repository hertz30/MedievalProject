using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ItemAttachment : MonoBehaviour
{
    public Transform handBone; // Reference to the character's hand bone
    public FMODUnity.EventReference MyEvent;
    void Awake()
    {
        // Set the hand bone as the new parent
        transform.parent = handBone;
        // Reset local position and rotation to keep the item's original appearance
        transform.localPosition = new Vector3(0.13f, 0f, 0f);
    }
    void OnTriggerEnter (Collider coll){
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.CompareTag("Enemy")){
            collidedWith.GetComponent<EnemyScript>().health-=.35f;
            Debug.Log("health:"+collidedWith.GetComponent<EnemyScript>().health);
            FMODUnity.RuntimeManager.PlayOneShot(MyEvent, transform.position);
        }
    }
}

