using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSprint : IState
{
    private BossReferences boss;
    private PlayerReference playerReference;
    private AudioManager audio;

    float timer;
    public BossSprint(BossReferences boss, PlayerReference playerReference, AudioManager audio)
    {
        this.boss = boss;
        this.playerReference = playerReference;
        this.audio = audio;
    }

    public void OnEnter()
    {
        timer = 0.5f;
        boss.animator.SetBool("Sprint", true);
    }
    public void OnExit()
    {
        Debug.Log("OnExit is bing called");
        boss.animator.SetBool("Sprint", false);
    }
    public void Tick()
    {
        
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, playerReference.transform.position, 16f * Time.deltaTime);
        

        Vector3 targetDirection = playerReference.transform.position - boss.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(boss.transform.forward, targetDirection, 5f * Time.deltaTime, 0);

        boss.transform.rotation = Quaternion.LookRotation(newDirection);
        
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            audio.Walk();
            timer = 0.45f;
        }

    }
}
