using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    private float speed;
    private bool isDead;
    private bool isAttacking;
    //distance for enemy to start searching for player
    public float lookRadius = 20f;
    public float health = 1;
    NavMeshAgent agent;
    private Animator anim;
    Transform target;
    public GameObject myPrefab;
    private Slider healthBar;
    public float attackRate=2f;
    public float damage = 0.1f;
    private float nextAttack;
    private static GameObject player;
    // Start is called before the first frame update
    void Start(){
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        (Instantiate (myPrefab, this.transform.position, this.transform.rotation) as GameObject).transform.parent = this.transform;
        anim = transform.GetChild(1).GetComponent<Animator>();
        anim.SetInteger("MeleeType_int", UnityEngine.Random.Range(0,3));
        anim.SetInteger("Animation_int", UnityEngine.Random.Range(1,9));
        anim.SetInteger("DeathType_int", UnityEngine.Random.Range(1,3));
        healthBar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbar();
        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("Death_b", isDead);
            Invoke("Dead", 1.4f);
        }
        if (!isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                speed = 0.6f;
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    //face the target
                    FaceTarget();
                }
            }
            else
            {
                anim.SetInteger("Animation_int", UnityEngine.Random.Range(1, 9));
                speed = 0;
                agent.SetDestination(transform.position);
            }
            anim.SetFloat("Speed_f", speed);
            if (isAttacking && Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                player.GetComponent<PlayerCC>().health -= damage;
                Debug.Log("attacked");
            }
        }
    }
    public void UpdateHealthbar(){
        healthBar.value = health;
    }
    void Dead(){
        anim.speed=0;
    }
    void OnTriggerEnter(Collider coll) {
            GameObject obj = coll.gameObject;
            if (obj.CompareTag("Player"))
            {
                isAttacking = true;
                anim.SetBool("Is_Attacking", true);
            }
        
    }
    private void OnTriggerExit(Collider coll) {
        GameObject obj = coll.gameObject;
        if (obj.CompareTag("Player")){
            isAttacking=false;
            anim.SetBool("Is_Attacking", true);
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
