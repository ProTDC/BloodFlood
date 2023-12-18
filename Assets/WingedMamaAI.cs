using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedMamaAI : MonoBehaviour
{
    private Transform target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float minY = 5f;  // Minimum y-axis value
    public float maxY = 15f; // Maximum y-axis value

    public Transform enemyGFX;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            Vector3 adjustedTargetPosition = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minY, maxY), target.position.z);
            seeker.StartPath(rb.position, adjustedTargetPosition, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (force.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }
        else if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }
}
