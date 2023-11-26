using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseColliderScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI objective;
    public AudioSource playerAudio;
    public AudioClip changeObjectiveSound;
    
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
}
