using UnityEngine;

public class EnemyAttack : IState
{
    private EnemyReferences enemyReferences;
    private PlayerReference playerReference;
    public EnemyAttack(EnemyReferences enemyReferences, PlayerReference playerReference)
    {
        this.enemyReferences = enemyReferences;
        this.playerReference = playerReference;
    }
    public void OnEnter()
    {
        enemyReferences.animator.SetBool("isAttacking", true);
    }
    public void OnExit()
    {
        enemyReferences.animator.SetBool("isAttacking", false);
    }
    public void Tick()
    {
        // if(!enemyReferences.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attacking"))
        // {
        //     enemyReferences.navMeshagent.SetDestination(playerReference.transform.position);
        // }

        enemyReferences.navMeshagent.SetDestination(playerReference.transform.position);
    }

    public bool isGone()
    {
        return enemyReferences.navMeshagent.remainingDistance > 3.5f;
    }
    
}
