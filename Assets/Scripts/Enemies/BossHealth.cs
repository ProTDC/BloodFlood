using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    public ParticleSystem lifeBlood;
    public ParticleSystem deathBlood;
    private LevelSystem levelSystem;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private float invulnerabilityTime = .2f;

    private bool hit;
    public int health = 500;
    public bool giveUpwardForce = true;
    public float knockBackForce = 10;
    public float knockBackForceUp = 2;
    public int levelXP;

    public bool isInvulnerable = false;

    public void TakeDamage(int damage, GameObject sender)
    {
        if (isInvulnerable)
        {
            return;
        }

        OnHitWithReference?.Invoke(sender);
        health -= damage;
        var boom = Instantiate(lifeBlood, transform.position, Quaternion.identity);
        boom.Play();

        if (health <= 0)
        {
            OnDeathWithReference?.Invoke(sender);
            Die();
        }
    }

    public void Die()
    {
        levelSystem.AddExperience(levelXP);
        var deathBoom = Instantiate(deathBlood, transform.position, Quaternion.identity);
        deathBoom.Play();
        Destroy(gameObject);
    }

    private IEnumerator TurnOffHit()
    {
        yield return new WaitForSeconds(invulnerabilityTime);

        hit = false;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }
}
