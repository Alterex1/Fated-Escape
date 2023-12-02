using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount, minSpeed = 10, maxSpeed = 16;
    public float timeBetweenSpawn;
    public List<GameObject> enemies;
    public float[] probabilities;
    public bool enabled;
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
        yield return new WaitUntil(() => enabled);
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);

            float curProbability = 1, spawnNum = Random.value;
            for (int j = 0; j < enemies.Count; j++)
            {
                curProbability -= probabilities[j];
                if (spawnNum >= curProbability)
                {
                    GameObject clone = Instantiate(enemies[j], transform.position, transform.rotation);
                    clone.SetActive(true);
                    clone.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = Random.Range(minSpeed, maxSpeed);
                    break;
                }
            }
        }
    }
}
