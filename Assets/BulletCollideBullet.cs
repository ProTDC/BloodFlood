using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollideBullet : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3, true);
    }
}
