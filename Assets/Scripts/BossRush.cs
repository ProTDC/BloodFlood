using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRush : MonoBehaviour
{
    public float rushPower = 24;
    public int rushDirection = -1;
    Rigidbody2D body;
    TrailRenderer trail;
    Boss boss;
    BoxCollider2D box;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        boss = GetComponent<Boss>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (boss.isFlipped == true)
        {
            rushDirection = 1;
        }
        else
        {
            rushDirection = -1;
        }
    }

    public void Rush()
    {
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        trail.emitting = true;
        body.velocity = new Vector2(rushDirection * rushPower, 0f);
        yield return new WaitForSeconds(1f);
        body.gravityScale = originalGravity;
        trail.emitting = false;
    }
}
