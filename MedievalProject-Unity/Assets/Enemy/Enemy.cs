using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    //distance for enemy to start searching for player
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    private Animator animate;


    void Start(){
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animate = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius){
            agent.SetDestination(target.position);
            animate.SetBool("isAttacking", false);
            animate.SetBool("foundTarget", true);
            
            if(distance <= agent.stoppingDistance){
                //face the target
                FaceTarget();
                animate.SetBool("isAttacking", true);
                animate.SetBool("foundTarget", true);
            }
        } else {
            animate.SetBool("isAttacking", false);
            animate.SetBool("foundTarget", false);
        }
    }
    void FaceTarget(){
        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
