using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Transform target;
    EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.IsDead()){
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked){
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange){
            isProvoked = true;

        }
    }

    public void OnDamageTaken(){
        isProvoked = true;
    }

    private void EngageTarget(){
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance){
            //Chase target
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance){
            //Attack target
            AttackTarget();
        }
    }

    void ChaseTarget(){
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget(){
        GetComponent<Animator>().SetBool("Attack", true);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized; 
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * turnSpeed);
    }
}
