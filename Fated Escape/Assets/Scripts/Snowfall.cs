using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowfall : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.transform.position + new Vector3(0, 15, 0);
        toggleSnow();
    }

    // Pause / Play particles based on the player being underneath an object
    private void toggleSnow()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.up), out hit))
            GetComponent<ParticleSystem>().Stop();
        else
            GetComponent<ParticleSystem>().Play();
    }
}
