using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : IState
{
    private BossReferences bossReferences;
    private PlayerReference playerReference;
    private AudioManager audio;
    float animationTime;
    public BossCharge(BossReferences bossReferences, PlayerReference playerReference, AudioManager audio)
    {
        this.bossReferences = bossReferences;
        this.playerReference = playerReference;
        this.audio = audio;
    }
    public void OnEnter()
    {
        animationTime = 0.3f;
        bossReferences.animator.SetBool("Sprint", false);
        
    }
    public void OnExit()
    {
        bossReferences.animator.SetBool("Sprint", true);
    }
    public void Tick()
    {
        animationTime -= Time.deltaTime;

        if (animationTime < 0.0f)
        {
            audio.Hit();
            playerReference.GetComponent<PlayerHealth>().TakeDamage(40);
            playerReference.GetComponent<Rigidbody>().AddForce(0, 0, -1000, ForceMode.Force);
            animationTime = 1.0f;
            
        }
        Vector3 targetDirection = playerReference.transform.position - bossReferences.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(bossReferences.transform.forward, targetDirection, 5f * Time.deltaTime, 0);

        bossReferences.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
