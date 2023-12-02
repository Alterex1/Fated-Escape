using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooterAudio : MonoBehaviour
{
    AudioSource shooterAudio;
    // Start is called before the first frame update
    void Start()
    {
        shooterAudio = GetComponent<AudioSource>();
        shooterAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
