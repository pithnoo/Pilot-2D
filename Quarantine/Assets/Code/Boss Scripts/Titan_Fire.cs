using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Fire : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public GameObject lazerShot;
    public GameObject lazer;
    private Transform lazerPoint;
    public AudioManager theAudioManager;
    private Titan_Link titanLink;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        theAudioManager = animator.gameObject.GetComponent<Titan_Link>().theAudioManager;
        lazerPoint = animator.GetComponent<Titan_Link>().lazerShotPoint;

        Vector2 lazerPosition = new Vector2(lazerPoint.position.x + 9, lazerPoint.position.y);
        Vector2 LazerShotPosition = new Vector2(lazerPoint.position.x - 16, lazerPoint.position.y);

        Instantiate(lazer, lazerPosition, lazerPoint.rotation);
        Instantiate(lazerShot, LazerShotPosition, lazerPoint.rotation);
        theAudioManager.Play("LazerFire");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Fire");
    }

}
