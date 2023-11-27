using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    private BossReferences bossReferences;
    private PlayerReference playerReference;
    private StateMachine stateMachine;
    public int health =  500;

    // Start is called before the first frame update
    private void Awake()
    {
        bossReferences = GetComponent<BossReferences>();
        playerReference = FindObjectOfType<PlayerReference>();

        stateMachine = new StateMachine();

        var runToPlayer = new BossRun(bossReferences, playerReference);
        var attackPlayer = new BossAttack(bossReferences, playerReference);
        var death = new BossDeath(bossReferences, playerReference);

        

        At(runToPlayer, attackPlayer, HasArrived());
        At(attackPlayer, runToPlayer, isGone());
        //Any(death, IsDead());

        stateMachine.SetState(runToPlayer);
        
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

        Func<bool> HasArrived() => () => bossReferences.navMeshagent.remainingDistance < 3f;
        Func<bool> isGone() => () => bossReferences.navMeshagent.remainingDistance > 2f;
        Func<bool> Half() => () => health <= 250;
        Func<bool> IsDead() => () => health < 0;
        



    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }
}
