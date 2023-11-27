using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : IState
{
    private BossReferences boss;
    private PlayerReference playerReference;
    public BossDeath(BossReferences bossReferences, PlayerReference playerReference)
    {
        this.boss = bossReferences;
        this.playerReference = playerReference;
    }

    public void OnEnter()
    {
        boss.animator.SetBool("isAlive", false);
    }
    public void OnExit()
    {
        
    }
    public void Tick()
    {
    }

}
