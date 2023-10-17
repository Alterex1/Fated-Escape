using UnityEngine;

public class EnemyAttack2 : IState
{
    private EnemyReferences2 enemyReferences;
    private PlayerReference playerReference;
    public EnemyAttack2(EnemyReferences2 enemyReferences, PlayerReference playerReference)
    {
        this.enemyReferences = enemyReferences;
        this.playerReference = playerReference;
    }
    public void OnEnter()
    {
        enemyReferences.animator2.SetBool("isAttacking", true);
    }
    public void OnExit()
    {
        enemyReferences.animator2.SetBool("isAttacking", false);
    }
    public void Tick()
    {
        // if(!enemyReferences.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attacking"))
        // {
        //     enemyReferences.navMeshagent.SetDestination(playerReference.transform.position);
        // }

        enemyReferences.navMeshagent2.SetDestination(playerReference.transform.position);
    }

    public bool isGone()
    {
        return enemyReferences.navMeshagent2.remainingDistance > 3.5f;
    }
    
}
