using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStars : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }
}
