using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class EnemyRun2 : IState
{
    private EnemyReferences2 enemy1;
    private PlayerReference playerReference;
    public EnemyRun2(EnemyReferences2 enemyReferences, PlayerReference playerReference)
    {
        this.enemy1 = enemyReferences;
        this.playerReference = playerReference;
    }

    public void OnEnter()
    {
        enemy1.navMeshagent2.SetDestination(playerReference.transform.position);
    }
    public void OnExit()
    {
        enemy1.animator2.SetFloat("Speed", 0f);
    }
    public void Tick()
    {
        enemy1.navMeshagent2.SetDestination(playerReference.transform.position);
        enemy1.animator2.SetFloat("Speed", enemy1.navMeshagent2.desiredVelocity.sqrMagnitude);
    }

    public bool HasArrived()
    {
        return enemy1.navMeshagent2.remainingDistance < 2f;
    }
    
}
