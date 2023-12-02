using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class EnemyRun : IState
{
    private EnemyReferences enemy1;
    private PlayerReference playerReference;
    public EnemyRun(EnemyReferences enemyReferences, PlayerReference playerReference)
    {
        this.enemy1 = enemyReferences;
        this.playerReference = playerReference;
    }

    public void OnEnter()
    {
        enemy1.navMeshagent.SetDestination(playerReference.transform.position);
    }
    public void OnExit()
    {
        enemy1.animator.SetFloat("Speed", 0f);
    }
    public void Tick()
    {
        enemy1.navMeshagent.SetDestination(playerReference.transform.position);
        enemy1.animator.SetFloat("Speed", enemy1.navMeshagent.desiredVelocity.sqrMagnitude);
    }

    public bool HasArrived()
    {
        return enemy1.navMeshagent.remainingDistance < 2f;
    }
    
}
