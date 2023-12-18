using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private TextMeshProUGUI skillPointsText;
    [SerializeField] private PlayerSkills playerSkills;

    private void Awake()
    {
        skillPointsText = transform.Find("Currency").GetComponent<TextMeshProUGUI>();
    }
} 
