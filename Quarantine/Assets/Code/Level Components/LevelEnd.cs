using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public string levelToLoad;
    private PlayerController Player;
    private LevelManager theLevelManager;
    public GameObject levelEndParticle;

    private SpriteRenderer theSpriteRenderer;

    public bool isOpen, isOpenStart;
    private Animator Anim;

    private LevelLoader theLevelLoader;
    
    public AudioManager theAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        theLevelManager = FindObjectOfType<LevelManager>();
        theLevelLoader = FindObjectOfType<LevelLoader>();

        theSpriteRenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();

        theAudioManager = FindObjectOfType<AudioManager>();

        isOpen = false;
        isOpenStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("isOpen", isOpen);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Anim.SetTrigger("isOpenStart");
            isOpen = true;
            theAudioManager.Play("CheckPoint");

            StartCoroutine("LevelEndCo");
            //Application.LoadLevel(levelToLoad);          
            theLevelLoader.LoadNextLevel();
        }
    }
    public IEnumerator LevelEndCo(){
        Instantiate(levelEndParticle, transform.position, transform.rotation);
        Player.gameObject.SetActive(false);

        //Player.canMove = false;
        theLevelManager.invincible = true;

        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);

        yield return new WaitForSeconds(1);
        
    }
}
