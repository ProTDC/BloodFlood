using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 3f;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            HandleCollision(collision.GetComponent<PlayerMovement>());
        }
    }

    private void HandleCollision(PlayerMovement player)
    {
        player.Damage(attackDamage);

        StartCoroutine(NoLongerColliding());
    }

    private IEnumerator NoLongerColliding()
    {
        yield return new WaitForSeconds(6);
    }

}
