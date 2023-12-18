using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kills : MonoBehaviour
{
    public int maxKills;
    public int killsAchived;

    private Dictionary<Transform, bool> enemyKilledStatus = new Dictionary<Transform, bool>();

    private void Start()
    {
        maxKills = this.transform.childCount;

        foreach (Transform child in transform)
        {
            enemyKilledStatus[child] = false;
        }
    }

    private void Update()
    {
        foreach (Transform child in transform)
        {
            EnemyHealth health = child.GetComponent<EnemyHealth>();

            if (health.currentHealth <= 0 && !enemyKilledStatus[child])
            {
                AddKill();
                enemyKilledStatus[child] = true;
            }
        }
    }

    void AddKill()
    {
        killsAchived++;
    }
}
