using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseColliderScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI objective;
    public AudioSource playerAudio;
    public AudioClip changeObjectiveSound;

    public BaseColliderScript baseCollider;
    public string text;
    public EnemySpawner enemySpawner;
    private bool alreadyTriggered;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setObjectiveText(string text)
    {
        playerAudio.PlayOneShot(changeObjectiveSound);
        objective.text = text;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered)
        {
            baseCollider.setObjectiveText(text);
            if (enemySpawner != null)
                enemySpawner.enabled = true;
            alreadyTriggered = true;
        }
    }
}
