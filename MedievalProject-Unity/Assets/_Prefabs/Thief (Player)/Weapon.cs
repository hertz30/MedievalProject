using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ItemAttachment : MonoBehaviour
{
    public Transform handBone; // Reference to the character's hand bone
    public FMODUnity.EventReference MyEvent;
    public float hitCooldown = 0.2f; // Time before the same enemy can be hit again
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
    void Awake()
    {
        // Set the hand bone as the new parent
        transform.parent = handBone;
        // Reset local position and rotation to keep the item's original appearance
        transform.localPosition = new Vector3(0.13f, 0f, 0f);
    }
    void OnTriggerEnter (Collider coll){
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.CompareTag("Enemy"))
        {
            // If the enemy is NOT in the list, we can hit them
            if (!hitEnemies.Contains(collidedWith))
            {
                ApplyDamage(collidedWith);
                
                // Start the cooldown process for THIS specific enemy
                StartCoroutine(RemoveFromHitList(collidedWith, hitCooldown));
            }
        }
    }

    void ApplyDamage(GameObject enemy)
    {
        hitEnemies.Add(enemy); // Add to "don't hit again" list
        
        EnemyScript script = enemy.GetComponent<EnemyScript>();
        if (script != null)
        {
            script.health -= 0.35f;
            Debug.Log("health: " + script.health);
        }

        FMODUnity.RuntimeManager.PlayOneShot(MyEvent, transform.position);
    }

    IEnumerator RemoveFromHitList(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        hitEnemies.Remove(enemy);
    }
}