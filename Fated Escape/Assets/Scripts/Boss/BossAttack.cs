using UnityEngine;

public class BossAttack : IState
{
    private BossReferences bossReferences;
    private PlayerReference playerReference;
    float animationTime = 3.0f;
    public BossAttack(BossReferences bossReferences, PlayerReference playerReference)
    {
        this.bossReferences = bossReferences;
        this.playerReference = playerReference;
    }
    public void OnEnter()
    {
        bossReferences.animator.SetBool("isWalking", false);
    }
    public void OnExit()
    {
        bossReferences.animator.SetBool("isWalking", true);
    }
    public void Tick()
    {
        // if(!enemyReferences.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attacking"))
        // {
        //     enemyReferences.navMeshagent.SetDestination(playerReference.transform.position);
        // }
        animationTime -= Time.deltaTime;

        if (bossReferences.navMeshagent.remainingDistance  < 6f)
        {
            
        }
        if (animationTime < 0.0f)
        {
            playerReference.GetComponent<PlayerHealth>().TakeDamage(40);
            
        }
    }

    
    
}
