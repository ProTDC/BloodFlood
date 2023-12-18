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
    }

    public void StartParry()
    {
        isParrying = true;
        startTime = Time.time;
        animator.SetTrigger("ParryStart");
        spriteRenderer.color = Color.yellow;
    }

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
            EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();
            EnemyDamage enemyDmg = collision.gameObject.GetComponent<EnemyDamage>();

            enemyHealth.Damage(enemyDmg.damage, transform.gameObject);
        }
    }


}
