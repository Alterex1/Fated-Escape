using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform playerTransform;
    public Animator enemyAnimator;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float damage = 5f;
    NavMeshAgent agent;
    float timer = 0.0f;

    private int health = 100;

    private float lastAttack = 0;
    private const float attackSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lastAttack += Time.deltaTime;

        timer -= Time.deltaTime;
        if(timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).magnitude;
            if(sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            
            timer = maxTime;
        }
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void attackPlayer() {
        // Make sure that enemy can't spam attacks
        if (lastAttack >= attackSpeed)
        {
            lastAttack = 0;
            enemyAnimator.SetFloat("Speed", 0f);
            enemyAnimator.SetBool("isAttacking", true);
            playerTransform.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerTransform.gameObject)
        {
            attackPlayer();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == playerTransform.gameObject)
        {
            attackPlayer();
        }
    }

    private void OnCollisionExit()
    {
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
