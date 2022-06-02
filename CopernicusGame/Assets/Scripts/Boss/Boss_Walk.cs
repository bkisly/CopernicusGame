using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    public float AttackRangeTrigger = 2f;

    private float _timer = 0f;
    //public GameObject boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        animator.gameObject.GetComponent<Boss>().Move();

        if(_timer >= 2f)
        {
            _timer = 0;
            animator.SetBool("IsJumping", true);
        }

        if(Vector2.Distance(animator.transform.position, animator.GetComponent<Boss>().Player.transform.position) < AttackRangeTrigger)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
