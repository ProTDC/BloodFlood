using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotationUI : MonoBehaviour
{
    float lockRot;
    RectTransform transf;

    // Use this for initialization
    void Start()
    {
        transf = GetComponent<RectTransform>();
        lockRot = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock rotation on x and y axes to 0
        transf.rotation = Quaternion.Euler(lockRot, lockRot, transform.rotation.eulerAngles.z);
    }
}
