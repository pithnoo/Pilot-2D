using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject hazard;
    public Animator animator;
    void Start(){
        animator = GetComponent<Animator>();
    }

    public void bombDrop(){
        Instantiate(hazard, transform.position, transform.rotation);
    }    
    public void bombReset(){
        animator.ResetTrigger("Bomb");
    }
}
