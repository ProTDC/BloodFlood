using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;
    public float gunRange = 5f;
    public int gunChance = 0;
    public float spinRange = 10f;

    Transform player;
    Rigidbody2D rb;
    Boss boss;

    Animator weapon;

    // onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        weapon = animator.GetComponentInChildren<BossWeapon>().GetComponent<Animator>();
    }

    // onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        boss.LookAtPlayer();
        
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            weapon.SetTrigger("Strike");
        }

        if (Vector2.Distance(player.position, rb.position) >= gunRange)
        {
            animator.SetTrigger("Gun");
        }

        if (Vector2.Distance(player.position, rb.position) >= spinRange)
        {
            //animator.SetTrigger("Spin");
            animator.SetTrigger("DownStrike");
        }
    }

    // onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Gun");
        animator.ResetTrigger("Spin");
    }
}
