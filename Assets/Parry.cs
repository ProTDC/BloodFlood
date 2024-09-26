using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private PlayerMovement player;
    private Rigidbody2D playerRigidbody;
    
    public float parryWindow = 0.4f;

    public float parryCooldown = 1.0f;
    private float lastParryTime;

    public bool isParrying = false;
    private float startTime;
    private float currentTimeScale;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();    
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentTimeScale = Time.timeScale;
    }

    private void Update()
    {
        if (isParrying && Time.time - startTime >= parryWindow)
        {
            EndParry();
        }

        player.CheckDash();
    }

    //Parry?
    public void StartParry()
    {
        isParrying = true;
        startTime = Time.time;
        animator.SetTrigger("ParryStart");
        spriteRenderer.color = Color.yellow;
    }

    //PARRY THE PLAITPUS!??!??
    private void EndParry()
    {
        isParrying = false;
        spriteRenderer.color = Color.white;
        animator.SetBool("ParryStart", false);
        animator.SetBool("ParrySuccess", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isParrying)
        {
            if (IsSuccessfulParry(collision))
            {
                ExecuteParryAction(collision);
            }
            else
            {
                EndParry();
            }
        }
    }

    private bool IsSuccessfulParry(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Bullet"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ExecuteParryAction(Collision2D collision)
    {
        Debug.Log("Successful parry!");
        animator.SetBool("ParrySuccessful", true);

        if (collision.transform.CompareTag("Enemy"))
        {
            StartCoroutine(ParryDash());
            
            //EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
            //EnemyDamage enemyDmg = collision.gameObject.GetComponent<EnemyDamage>();

            //enemyHealth.Damage(enemyDmg.damage, transform.gameObject);
        }
    }

    private IEnumerator ParryDash()
    {
        player.dashTimeLeft = player.dashingTime;
        player.lastDash = Time.time;
        player.canDash = false;
        player.isDashing = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        float originalGravity = player.body.gravityScale;
        player.body.gravityScale = 0f;

        Vector2 dashDirectionVector = new Vector2(player.dashDirection, 0f);
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dashDirectionVector, 1f, player.wallLayer);
        PlayerAfterImagePool.Instance.GetFromPool();
        player.lastImageXpos = transform.position.x;

        if (wallHit.collider == null)
        {
            player.body.linearVelocity = new Vector2(player.dashDirection * player.dashingPower, 0f);
            player.dashTimeLeft -= Time.deltaTime;
            player.trail.emitting = true;
            animator.SetTrigger("BetterDash");
            yield return new WaitForSeconds(player.dashingTime);
            player.trail.emitting = false;
            player.body.gravityScale = originalGravity;
            player.isDashing = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            yield return new WaitForSeconds(player.dashingCooldown);
        }
        else
        {
            player.isDashing = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
        player.canDash = true;
    }


}
