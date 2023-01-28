using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //Health and Damage
    public float health = 100f;
    public float damage = 50f;
    public int enemyWorth = 5;
    public TargetHealth targetHP;
    public GameObject targetObj;
    public GameManager manager;
    public Rigidbody rb;

    //Pathfinding Variables
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform target;

    private void Awake(){
        targetObj = GameObject.Find("Target");
        targetHP = targetObj.GetComponent<TargetHealth>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        target = targetObj.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start(){
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collider)
    {
        //Debug.Log("Collision");
        if(collider.transform.tag == "Player"){
            targetHP.TakeDamage(damage);
            Die();
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    private void Die(){
        //play some particle effect and sound
        manager.enemyCnt--;
        manager.resources += enemyWorth;
        Destroy(gameObject);
    }
}
