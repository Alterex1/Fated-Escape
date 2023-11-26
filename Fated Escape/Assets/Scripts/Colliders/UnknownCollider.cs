using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownCollider : MonoBehaviour
{
    public BaseColliderScript baseCollider;
    private bool alreadyTriggered;
    // Start is called before the first frame update
    void Start()
    {
        alreadyTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered)
        {
            baseCollider.setObjectiveText("???");
            alreadyTriggered = true;
        }
    }
}
