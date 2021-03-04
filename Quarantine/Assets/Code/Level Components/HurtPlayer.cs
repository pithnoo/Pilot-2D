using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private LevelManager theLevelManager;

    public int damageToGive;
    public AudioManager theAudioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();   
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //thelevelManager.Respawn();

            theLevelManager.HurtPlayer(damageToGive);
            theAudioManager.Play("HitHurt");

            var player = other.GetComponent<PlayerController>();
            player.knockbackCount = player.knockbackLength;

            if(other.transform.position.x < transform.position.x)
            {
                player.knockRight = true;
            }
            else
            {
                player.knockRight = false;
            }
        }
    }
}
