using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Process : MonoBehaviour
{
    private Animator animator;
    public bool isActive;
    public enum bossAction{idle,lazer,bomb,missile,slam}
    private bossAction eCurState = bossAction.idle;
    public float attackTime = 0f;
    public AudioManager theAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        theAudioManager = FindObjectOfType<AudioManager>();
        isActive = true;
    }

    public void setStates() {
        switch(eCurState){
            case bossAction.idle:
                break;
            case bossAction.lazer:
                animator.SetTrigger("Fire");
                break;
            case bossAction.bomb:
                animator.SetTrigger("Bomb");
                break;
            case bossAction.missile:
                if(GameObject.FindGameObjectWithTag("Player") != null){
                    animator.SetTrigger("Missile");
                }
                break;
            case bossAction.slam:
                animator.SetTrigger("Slam");
                break;       
        }
    }

    public void slam(){
        eCurState = bossAction.slam;
    }

    public void attackRoll(){
        int randomAttack = Random.Range(1, 4);
        switch (randomAttack)
        {
            case 1:
                eCurState = bossAction.lazer;
                theAudioManager.Play("LazerCharge");
                //Debug.Log("c1");
                break;
            case 2:
                eCurState = bossAction.bomb;
                //Debug.Log("c2");
                break;
            case 3:
                eCurState = bossAction.missile;
                //Debug.Log("c3");
                break;
        }
    }
     
}
