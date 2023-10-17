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
    public float damage = 20f;
    NavMeshAgent agent;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(playerTransform);
        Debug.Log(collision.gameObject);

        if(collision.gameObject == playerTransform.gameObject)
        {
            Debug.Log("Hello");
            enemyAnimator.SetFloat("Speed", 0f);
            enemyAnimator.SetBool("isAttacking", true);
            playerTransform.GetComponent<PlayerManager>().Hit(damage);
            
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == playerTransform.gameObject)
        {
            enemyAnimator.SetBool("isAttacking", true);
            playerTransform.GetComponent<PlayerManager>().Hit(damage);
            enemyAnimator.SetFloat("Speed", 0f);
        }
    }

    private void OnCollisionExit()
    {
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
