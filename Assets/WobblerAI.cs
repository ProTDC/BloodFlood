using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WobblerAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float wallAvoidanceDistance = 2f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                Vector3 avoidanceDirection = Vector3.Cross(Vector3.up, hit.normal).normalized;
                Vector3 desiredDirection = direction + avoidanceDirection * wallAvoidanceDistance;
                MoveTowards(desiredDirection);

                FaceMovementDirection(desiredDirection);
            }
            else
            {
                MoveTowards(direction);

                FaceMovementDirection(direction);
            }
        }
    }

    void MoveTowards(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void FaceMovementDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
