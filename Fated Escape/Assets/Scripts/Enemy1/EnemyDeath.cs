using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : IState
{
    private EnemyReferences enemy1;
    private PlayerReference playerReference;
    public EnemyDeath(EnemyReferences enemyReferences, PlayerReference playerReference)
    {
        this.enemy1 = enemyReferences;
        this.playerReference = playerReference;
    }

    public void OnEnter()
    {
        enemy1.animator.SetBool("isAlive", false);
    }
    public void OnExit()
    {
        
    }
    public void Tick()
    {
    }

}
