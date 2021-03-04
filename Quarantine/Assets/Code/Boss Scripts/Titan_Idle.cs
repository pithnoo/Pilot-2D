using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Idle : StateMachineBehaviour
{
    private Titan_Process TitanProcess;
    public int attacks = 0;
    public int numAttacks;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TitanProcess = animator.gameObject.GetComponent<Titan_Process>();
        if(attacks < numAttacks){
            TitanProcess.attackRoll();
            attacks++;
        }
        else{
            TitanProcess.slam();
            attacks = 0;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        TitanProcess.setStates();
    }

}
