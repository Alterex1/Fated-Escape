using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : IState
{
    private BossReferences boss;
    private AudioManager audio;
    public BossDeath(BossReferences bossReferences, AudioManager audio)
    {
        this.boss = bossReferences;
        this.audio = audio;
    }

    public void OnEnter()
    {
        boss.animator.SetBool("IsAlive", false);
        audio.Death();
        boss.GetComponent<BossStateMachine>().ComponentGone();

    }
    public void OnExit()
    {
        Debug.Log("he dead");
    }
    public void Tick()
    {
    }

}
