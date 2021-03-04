using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject Death;

    public Image heart1, heart2;
    public Sprite heartFull, heartHalf, heartEmpty;

    public Image lifeBox;
    public Sprite lifeFull, lifeHalf, lifeEmpty;
    public int startingLives, currentLives;

    public TextMeshProUGUI coinText;

    public int maxHealth, healthCount;

    public int coinCount;
    private int coinBonusLifeCount;

    private bool respawning;
    
    private ResetOnRespawn[] objectsToReset;

    public bool invincible;

    public GameObject gameOverScreen;

    public AudioManager theAudioManager;
    public bool hazBoxActive;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theAudioManager = FindObjectOfType<AudioManager>();

        coinCount = 0;

        if(PlayerPrefs.HasKey("PlayerLives")){
            currentLives = PlayerPrefs.GetInt("PlayerLives");
            //Debug.Log(currentLives);
        }else{
            currentLives = startingLives;
        } 

        updateLives();

        objectsToReset = FindObjectsOfType<ResetOnRespawn>(); //find objects should be used if referencing an array, as it stores multiple values

        coinText.text = "coins: " + coinCount;
        healthCount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        updateHealth();

        if(coinBonusLifeCount >= 50 && currentLives < 2)
        {
            theAudioManager.Play("PowerUp");
            currentLives++;
            updateLives();
            coinBonusLifeCount -= 50;
            coinCount = 0;
        }
    }

    public void Respawn()
    {
        currentLives -= 1;
        updateLives();

        //Debug.Log("active");

        if(currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;           
            theAudioManager.stopPlaying(theAudioManager.currentSong);
            theAudioManager.Play("HitHurt");
        }
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(Death, thePlayer.transform.position, thePlayer.transform.rotation);
        theAudioManager.Play("PlayerDeath");

        yield return new WaitForSeconds(waitToRespawn);
        healthCount = maxHealth;
        respawning = false;

        coinCount = 0;
        coinBonusLifeCount = 0;
        coinText.text = "coins: " + coinCount;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
        invincible = true;

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].ResetObject();
            objectsToReset[i].gameObject.SetActive(true);
            hazBoxActive = true;
        }
        yield return new WaitForSeconds(2);
        invincible = false;
        //respawning = false;
    }

    public void HurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            healthCount -= damageToTake;
            thePlayer.knockback();
            updateHealth();
        }
    }

    public void addCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;
        coinText.text = "coins: " + coinCount;
    }

    public void healthRestore(int health)
    {
        theAudioManager.Play("PowerUp");
        healthCount += health;
    }

    public void updateHealth()
    {
        switch(healthCount)
        {
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                return;
            case 3:
                heart1.sprite = heartHalf;
                heart2.sprite = heartFull;
                return;
            case 2:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartFull;
                return;
            case 1:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartHalf;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                return;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                return;

        }
    }

    public void updateLives()
    {
        switch (currentLives)
        {
            case 2:
                lifeBox.sprite = lifeFull;
                return;
            case 1:
                lifeBox.sprite = lifeHalf;
                return;
            case 0:
                lifeBox.sprite = lifeEmpty;
                return;
            default:
                lifeBox.sprite = lifeEmpty;
                return;               
        }
    }

}
