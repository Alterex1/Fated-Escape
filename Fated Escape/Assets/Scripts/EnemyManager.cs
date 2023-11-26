using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public List<AudioClip> hitSounds, deathSounds;
    public AudioSource enemyAudio;

    public Transform playerTransform;
    public Animator enemyAnimator;
    public NavMeshAgent agent;

    public float maxTime = 1.0f, maxDistance = 1.0f, damage = 5f, attackSpeed = 1.5f;
    public double health = 100;

    private float timer = 0.0f, lastAttack = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) return;

        lastAttack += Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).magnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            
            timer = maxTime;
        }
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void takeDamage(double damage)
    {
        health -= damage;
        if (health <= 0)
        {
            playerTransform.GetComponentInChildren<Weapon>().addKill();
            StartCoroutine(deathCoroutine());
        }
    }

    IEnumerator deathCoroutine()
    {
        PlayDeath();
        enemyAnimator.SetFloat("Speed", 0f);
        Destroy(GetComponent<NavMeshAgent>());
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void attackPlayer()
    {
        // Make sure that enemy can't spam attacks
        if (lastAttack >= attackSpeed)
        {
            lastAttack = 0;
            enemyAnimator.SetFloat("Speed", 0f);
            enemyAnimator.SetBool("isAttacking", true);
            PlayHit();
            playerTransform.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void PlaySound(List<AudioClip> availableSounds)
    {
        enemyAudio.pitch = Random.Range(0.95f, 1.05f);
        enemyAudio.PlayOneShot(availableSounds[Random.Range(0, availableSounds.Count)], Random.Range(0.5f, 1f));
    }

    private void PlayHit()
    {
        PlaySound(hitSounds);
    }

    private void PlayDeath()
    {
        AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Count)], this.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerTransform.gameObject) attackPlayer();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == playerTransform.gameObject) attackPlayer();
    }

    private void OnCollisionExit()
    {
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
