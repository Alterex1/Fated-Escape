using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCollider : MonoBehaviour
{
    public BaseColliderScript baseCollider;
    public string text;
    public List<EnemySpawner> enemySpawners;
    private bool alreadyTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered)
        {
            baseCollider.setObjectiveText(text);
            foreach (EnemySpawner spawner in enemySpawners)
                spawner.enabled = true;
            alreadyTriggered = true;
        }
    }
}
