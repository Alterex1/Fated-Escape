using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRage : IState
{
    private BossReferences bossReferences;
    private PlayerReference playerReference;
    
    private AudioManager audio;

    float animationTime;
    public BossRage(BossReferences bossReferences, PlayerReference playerReference, AudioManager audio)
    {
        this.bossReferences = bossReferences;
        this.playerReference = playerReference;
        this.audio = audio;
    }
    public void OnEnter()
    {
        animationTime = 3.00f;
        bossReferences.animator.SetBool("Rage", true);
        bossReferences.GetComponentInChildren<ParticleSystem>().Play(true);
        audio.Rage();
    }
    public void OnExit()
    {
        bossReferences.animator.SetBool("Rage", false);
        bossReferences.GetComponentInChildren<ParticleSystem>().Stop(true);
    }
    public void Tick()
    {
        animationTime -= Time.deltaTime;
        
    }

    public bool AnimationEnd()
    {
        return animationTime < 0;
    }
}
