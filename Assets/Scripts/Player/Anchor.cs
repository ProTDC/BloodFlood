using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private MeleeAttackManager meleeAttackManager;
    void Start()
    {
        meleeAttackManager = GetComponentInParent<MeleeAttackManager>();
    }
}
