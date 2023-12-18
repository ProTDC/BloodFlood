using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class WingedPatrol : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-0.2300756f, 0.2300756f, 0.2300756f);
        }
        else if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(0.2300756f, 0.2300756f, 0.2300756f);
        }
    }
}
