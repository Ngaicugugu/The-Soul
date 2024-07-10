using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBoss : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 2f;

    private int random;

    Transform player;
    Rigidbody2D rb;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

        random = Random.Range(0, 2);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(PlayerController.Instance.state == PlayerController.State.Playing)
        {
            boss.LookAtPlayer();

            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange && random==0)
            {
                animator.SetTrigger("attack");
            }else if(Vector2.Distance(player.position, rb.position) <= attackRange && random == 1)
            {
                animator.SetTrigger("skill");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.ResetTrigger("skill");
    }


}
