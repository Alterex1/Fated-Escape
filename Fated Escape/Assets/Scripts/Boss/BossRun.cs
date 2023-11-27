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
    float maxTime = 2.0f;
    float maxDistance = 10.0f;
    float timer = 0.0f;
    public BossRun(BossReferences boss, PlayerReference playerReference)
    {
        this.boss = boss;
        this.playerReference = playerReference;
    }

    public void OnEnter()
    {
        Debug.Log("OnEnter is being called");
        //boss.navMeshagent.SetDestination(playerReference.transform.position);
    }
    public void OnExit()
    {
        Debug.Log("OnExit is bing called");
        boss.animator.SetFloat("Speed", 0f);
    }
    public void Tick()
    {
        timer -= Time.deltaTime;
        if(!boss.navMeshagent.hasPath)
        {
            boss.navMeshagent.destination = playerReference.transform.position;
        }
      
        if(timer < 0.0f)
        {
            Vector3 direction = (playerReference.transform.position - boss.navMeshagent.destination);
            direction.y = 0;
            
            bool temp = direction.sqrMagnitude > (maxDistance * maxDistance);
            Debug.Log(temp);

            Debug.Log(direction.sqrMagnitude);
            if(direction.sqrMagnitude > maxDistance * maxDistance)
            {
                bool hello = boss.navMeshagent.pathStatus != NavMeshPathStatus.PathPartial;
                Debug.Log(hello + " in the other if");
                if (boss.navMeshagent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    boss.navMeshagent.destination = playerReference.transform.position;
                    Debug.Log(boss.navMeshagent.pathPending);
                }
            }
            timer = maxTime;
        }
        
        
        //boss.navMeshagent.SetDestination(playerReference.transform.position);
        boss.animator.SetFloat("Speed", 0.5f);
    }
}
