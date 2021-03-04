using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Fire_Missile : StateMachineBehaviour
{
    private Transform shotPoint;
    private AudioManager theAudioManager;
    public GameObject projectile;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shotPoint = animator.gameObject.GetComponent<Titan_Link>().shotPoint;
        theAudioManager = animator.gameObject.GetComponent<Titan_Link>().theAudioManager;

        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        theAudioManager.Play("MissileFire");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Missile");
    }
}
