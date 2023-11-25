using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public Transform playerTransform;
    public AudioClip interactionSound;
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
            AudioSource.PlayClipAtPoint(interactionSound, playerTransform.position, 0.75f);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
