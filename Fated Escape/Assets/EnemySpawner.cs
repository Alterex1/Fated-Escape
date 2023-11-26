using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount;
    public float timeBetweenSpawn;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            GameObject clone = Instantiate(enemy, transform.position, transform.rotation);
            Debug.Log("SPAWNING ENEMY");
            clone.SetActive(true);
        }
    }
}
