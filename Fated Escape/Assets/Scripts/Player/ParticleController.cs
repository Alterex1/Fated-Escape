using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particle_System;
    [HideInInspector]public Weapon weapon;  // Reference to the Weapon script

    void Start()
    {
        weapon = GameObject.FindObjectOfType<Weapon>();  // Find the Weapon script
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !weapon.isReloading && weapon.magazine > 0)
        {
            if (!particle_System.isPlaying)
            {
                particle_System.Play();
            }
        }
        else if (Input.GetMouseButtonUp(0) || weapon.isReloading || weapon.magazine <= 0)
        {
            particle_System.Stop();
        }
    }
}
