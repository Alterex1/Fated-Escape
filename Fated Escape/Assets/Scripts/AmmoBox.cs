using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerTransform.gameObject)
        {
            playerTransform.GetComponentInChildren<Weapon>().updateAmmo(90);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
