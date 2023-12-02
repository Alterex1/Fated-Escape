using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<string> inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(string item)
    {
        inventory.Add(item);
        Debug.Log("CURRENT INVENTORY: " + string.Join(",", inventory.ToArray()));
    }
}
