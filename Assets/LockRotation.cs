using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void Update()
    {
        var rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
        transform.rotation = rotation;
    }
}
