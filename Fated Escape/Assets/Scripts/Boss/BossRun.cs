using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class BossRun : IState
{
    private BossReferences boss;
    private PlayerReference playerReference;
    private AudioManager audio;
    float timer;
    public BossRun(BossReferences boss, PlayerReference playerReference, AudioManager audio)
    {
        this.boss = boss;
        this.playerReference = playerReference;
        this.audio = audio;
    }

    public void OnEnter()
    {
        timer = 0.5f;
        boss.animator.SetFloat("Speed", 0.5f);
    }
    public void OnExit()
    {
        Debug.Log("OnExit is bing called");
        boss.animator.SetFloat("Speed", 0f);
        audio.WalkStop();
    }
    public void Tick()
    {
        
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, playerReference.transform.position, 8f * Time.deltaTime);
        

        Vector3 targetDirection = playerReference.transform.position - boss.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(boss.transform.forward, targetDirection, 5f * Time.deltaTime, 0);

        //boss.navMeshagent.SetDestination(playerReference.transform.position);
        boss.transform.rotation = Quaternion.LookRotation(newDirection);

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            audio.Walk();
            timer = 0.6f;
        }

        
    }

}
