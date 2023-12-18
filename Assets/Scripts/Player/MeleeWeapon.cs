using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField]
    public int damageAmount;

    private PlayerMovement movement;
    private Rigidbody2D body;
    private MeleeAttackManager meleeAttackManager;
    private Vector2 direction;
    private bool collided;
    private bool downwardStrike;

    private void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        body = GetComponentInParent<Rigidbody2D>();
        meleeAttackManager = GetComponentInParent<MeleeAttackManager>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>())
        {
            HandleCollision(collision.GetComponent<EnemyHealth>());
        }
    }

    private void HandleCollision(EnemyHealth enemyHealth)
    {
        if (enemyHealth.giveUpwardForce && Input.GetAxis("Vertical") < 0 && !movement.isGrounded())
        {
            direction = Vector2.up;
            downwardStrike = true;
            collided = true;
        }

        if (Input.GetAxis("Vertical") > 0 && !movement.isGrounded() || Input.GetAxis("Vertical") == 0)
        {
            if (transform.parent.localScale.x < 0)
            {
                direction = Vector2.zero;
            }
            else
            {
                direction = Vector2.zero;
            }

            collided = true;
        }

        enemyHealth.Damage(damageAmount, transform.gameObject);
        movement.currentBlood += movement.bloodGain;
        movement.bloodbar.SetBlodd(movement.currentBlood);

        StartCoroutine(NoLongerColliding());
    }

    private void HandleMovement()
    {
        if (collided)
        {
            if (downwardStrike)
            {
                body.velocity = Vector2.zero;
                body.AddForce(direction * meleeAttackManager.upwardsForce);
            }
            else
            {
                body.AddForce(direction * meleeAttackManager.defaultForce);
            }
        }
    } 

    private IEnumerator NoLongerColliding()
    {
        yield return new WaitForSeconds(meleeAttackManager.movementTime);
        collided = false;
        downwardStrike = false;
    }

}
