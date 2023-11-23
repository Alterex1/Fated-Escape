using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    private float lastHit = 0;
    private const float invincibilitySeconds = 0.05f;

    public void Hit(float damage)
    {
        if (lastHit < invincibilitySeconds) return;
        lastHit = 0;
        health -= damage;
        Debug.Log(health);

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastHit += Time.deltaTime;
    }
}
