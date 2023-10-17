using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    private EnemyReferences enemyReferences;
    private PlayerReference playerReference;
    private StateMachine stateMachine;


    // Start is called before the first frame update
    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        playerReference = FindObjectOfType<PlayerReference>();

        stateMachine = new StateMachine();

        var runToPlayer = new EnemyRun(enemyReferences, playerReference);
        var attackPlayer = new EnemyAttack(enemyReferences, playerReference);

        
        var tempbool = runToPlayer.HasArrived();

        At(runToPlayer, attackPlayer, () => runToPlayer.HasArrived());
        At(attackPlayer, runToPlayer, () => attackPlayer.isGone());

        stateMachine.SetState(runToPlayer);
        
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }
}
