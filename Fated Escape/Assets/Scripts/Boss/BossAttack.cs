using UnityEngine;

public class BossAttack : IState
{
    private BossReferences bossReferences;
    private PlayerReference playerReference;
    private AudioManager audio;
    float animationTime;
    public BossAttack(BossReferences bossReferences, PlayerReference playerReference, AudioManager audio)
    {
        this.bossReferences = bossReferences;
        this.playerReference = playerReference;
        this.audio = audio;
    }
    public void OnEnter()
    {
        animationTime = 0.8f;
        bossReferences.animator.SetBool("isWalking", false);
    }
    public void OnExit()
    {
        bossReferences.animator.SetBool("isWalking", true);
    }
    public void Tick()
    {

        animationTime -= Time.deltaTime;

        if (animationTime < 0.0f)
        {
            audio.Hit();
            playerReference.GetComponent<PlayerHealth>().TakeDamage(40);
            animationTime = 2.4f;
            
        }
        Vector3 targetDirection = playerReference.transform.position - bossReferences.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(bossReferences.transform.forward, targetDirection, 5f * Time.deltaTime, 0);

        bossReferences.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    
    
}
