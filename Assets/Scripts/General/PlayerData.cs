using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float blood;
    public float[] position;

    public PlayerData (PlayerMovement player)
    {
        level = player.level;
        health = player.currentHealth;
        blood = player.currentBlood;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
