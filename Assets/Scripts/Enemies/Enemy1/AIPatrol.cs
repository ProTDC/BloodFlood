using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed, range, timeBtwChase, turnDelay;
    public float chaseSpeed;
    private float distToPlayer;
    public bool grounded;
    public float fallSpeed;

    [HideInInspector]
    public bool mustPatrol;
    public bool mustTurn, canChasing;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Collider2D bodyCollider;
    private Transform player;
    private Animator anim;


    void Start()
    {
        mustPatrol = true;
        canChasing = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (mustPatrol)
        {
            anim.SetBool("isChasing", false);
            Patrol();
        }

        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
            || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if (canChasing == true)
            {
                Chase();
            }
        }
        else
        {
            mustPatrol = true;
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

        grounded = Physics2D.OverlapArea(groundCheckPos.position, groundCheckPos.position, groundLayer);
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(wallLayer))
        {
            Flip();
        }

        rb.velocity = new Vector2(walkSpeed * Time.deltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator TurnWithDelay()
    {
        yield return new WaitForSeconds(turnDelay);
        Flip();
    }

    void Chase()
    {
        anim.SetBool("isChasing", true);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        canChasing = true;
    }

}
