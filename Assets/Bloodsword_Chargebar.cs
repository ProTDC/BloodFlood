using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloodsword_Chargebar : MonoBehaviour
{
    private MeleeAttackManager melee;
    private Slider slider;

    private void Start()
    {
        melee = GameObject.Find("Player").GetComponent<MeleeAttackManager>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        
    }
}
