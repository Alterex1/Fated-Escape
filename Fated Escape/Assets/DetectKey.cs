using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKey : MonoBehaviour
{
    public Transform playerTransform;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerTransform.gameObject)
        {
            playerTransform.GetComponent<InventoryManager>().addItem(name);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
