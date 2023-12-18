using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UI_SkillTree skillTree;

    private void Start()
    {
        skillTree.SetPlayerSkills(player.GetPlayerSkills());
    }
}
