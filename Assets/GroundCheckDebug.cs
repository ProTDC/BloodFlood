using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckDebug : MonoBehaviour
{
    public Transform groundCheckPos;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
