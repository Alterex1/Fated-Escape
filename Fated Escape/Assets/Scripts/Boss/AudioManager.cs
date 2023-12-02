using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource step;
    public AudioSource roar;
    public AudioSource death;
    public AudioSource hit;
    
    public void Walk()
    {
        step.Play();
    }

    public void WalkStop()
    {
        step.Stop();
    }

    public void Rage()
    {
        roar.PlayDelayed(0.25f);
    }

    public void Death()
    {
        death.Play();
    }

    public void Hit()
    {
        hit.Play();
    }

}
