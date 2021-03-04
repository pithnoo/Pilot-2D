using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject titanSlime;
    public GameObject stomp;
    public PlayerController thePlayer;
    public GameObject spawnPoint;
    public AudioManager theAudioManager;

    //public GameObject BG;
    //public GameObject bossMusic;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theAudioManager = FindObjectOfType<AudioManager>();
        //BG = GameObject.Find("BG music");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            thePlayer.respawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1, 0f);
            StartCoroutine("activeBoss");
        }
    }

    IEnumerator activeBoss(){
        //Destroy(BG);
        theAudioManager.stopPlaying("LevelTheme");
        yield return new WaitForSeconds(2);

        stomp.SetActive(true);
        titanSlime.SetActive(true);

        yield return new WaitForSeconds(2);

        theAudioManager.Play("BossTheme");
        theAudioManager.currentSong = "BossTheme";
        Destroy(gameObject);
        
        //bossMusic.SetActive(true);
    }
}
