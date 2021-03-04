using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Health : MonoBehaviour
{
    // Start is called before the first frame update
    public StompEnemy stomp;
    public int bossHealth = 4;
    public SpriteRenderer spriteRenderer;
    public Sprite health0, health1, health2, health3, health4;
    public Animator animator;
    public GameObject deathParticle, greenParticle, redParticle;
    public bool bossDead;
    public GameObject movingPlatform;

    //public GameObject bossMusic;
    void Start()
    {
        stomp = FindObjectOfType<StompEnemy>();   
        animator = GetComponent<Animator>();
        bossDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(stomp.bossDamage){
            animator.SetTrigger("Hurt");
            bossHealth -= 1;
            stomp.bossDamage = false;
        }

        switch(bossHealth){
            case 0:
                spriteRenderer.sprite = health0;
                if(bossDead == false){
                    animator.SetTrigger("Death");
                    movingPlatform.SetActive(true);
                    FindObjectOfType<AudioManager>().stopPlaying("BossTheme");
                    //bossMusic.SetActive(false);
                }
                break;
            case 1:
                spriteRenderer.sprite = health1;
                break;
            case 2:
                spriteRenderer.sprite = health2;
                break;
            case 3:
                spriteRenderer.sprite = health3;
                break;
            case 4:
                spriteRenderer.sprite = health4;
                break;
            default:
                spriteRenderer.sprite = health0;
                break;           
        }
    }


    public void complete(){
        Destroy(gameObject);
        Instantiate(deathParticle, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("BossDeath");
    }

    public void charge(){
        FindObjectOfType<AudioManager>().Play("LazerCharge");
    }

    public void green(){
        Instantiate(greenParticle, transform.position, transform.rotation);
    }

    public void red(){
        Instantiate(redParticle, transform.position, transform.rotation);
    }
}
