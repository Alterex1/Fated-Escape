using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    private BossReferences bossReferences;
    private PlayerReference playerReference;

    private new AudioManager audio;
    public StateMachine stateMachine;
    [HideInInspector]
    public float health;
    private float half;

    // Start is called before the first frame update
    private void Awake()
    {
        bossReferences = GetComponent<BossReferences>();
        playerReference = FindObjectOfType<PlayerReference>();
        audio = GetComponent<AudioManager>();

        health = 2000.0f;
        half = health/2;

        stateMachine = new StateMachine();

        var runToPlayer = new BossRun(bossReferences, playerReference, audio);
        var attackPlayer = new BossAttack(bossReferences, playerReference, audio);
        var rage = new BossRage(bossReferences, playerReference, audio);
        var sprintToPlayer = new BossSprint(bossReferences, playerReference, audio);
        var charge = new BossCharge(bossReferences, playerReference, audio);
        var death = new BossDeath(bossReferences, audio);

        bossReferences.GetComponentInChildren<ParticleSystem>().Stop(true);

        

        At(runToPlayer, attackPlayer, HasArrived());
        At(attackPlayer, runToPlayer, isGone());
        At(attackPlayer, rage, Half());
        At(runToPlayer, rage, Half());
        At(rage, sprintToPlayer, isGone2());
        At(sprintToPlayer, charge, HasArrived());
        At(charge, sprintToPlayer, isGone());
        Any(death, IsDead());

        stateMachine.SetState(runToPlayer);
        
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

        Func<bool> HasArrived() => () => Vector3.Distance(bossReferences.transform.position, playerReference.transform.position) < 12f;     
        Func<bool> isGone() => () => Vector3.Distance(bossReferences.transform.position, playerReference.transform.position) > 15f;
        Func<bool> isGone2() => () => Vector3.Distance(bossReferences.transform.position, playerReference.transform.position) > 13f && rage.AnimationEnd();
        Func<bool> Half() => () => health <= half;
        Func<bool> IsDead() => () => health <= 0.0f;
        



    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
        // Debug.Log(stateMachine._currentState);
        // Debug.Log(health);
    }

    public void ComponentGone()
    {
        Destroy(bossReferences.GetComponent<CapsuleCollider>());
    }
}
