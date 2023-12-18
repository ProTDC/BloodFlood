using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private bool damageable = true;

    [SerializeField]
    public int healthAmount = 100;

    [SerializeField]
    private float invulnerabilityTime = .2f;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    public enum enemyState
    {
        alive,
        dead
    }
    public enemyState currentState;

    public bool giveUpwardForce = true;
    private bool hit;
    public int currentHealth;
    public float knockBackForce = 10;
    public float knockBackForceUp = 2;
    public int levelXP;
    private bool isStunned = false;

    const float b_dropChance = 2f / 10f; 

    public ParticleSystem lifeBlood;
    public ParticleSystem deathBlood;
    public Rigidbody2D rb;
    //public GameObject playerGameObject;
    public GameObject bulletPickup;

    public Level_Bar level;
    private LevelSystem levelSystem;

    private SpriteRenderer sprite;
    private Color originalColor;

    private AudioManager audioManager;
    private EnemyKnockback knockback;

    public static EnemyHealth instance;

    private void Awake()
    {
        instance = this;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = healthAmount;

        if (GetComponent<SpriteRenderer>() == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        originalColor = sprite.color;

        knockback = GetComponent<EnemyKnockback>();
    }

    public void Damage(int amount, GameObject sender)
    {
        currentState = enemyState.alive;

        if (damageable && !hit && currentHealth > 0)
        {
            audioManager.PlaySFX(audioManager.playerBulletFlesh);
            rb.velocity = Vector2.zero;
            hit = true;
            currentHealth -= amount;
            sprite.color = new Color(1, 0, 0, 0.5f);
            var boom = Instantiate(lifeBlood, transform.position, Quaternion.identity);
            boom.Play();
            OnHitWithReference?.Invoke(sender);

            if (currentHealth <= 0)
            {
                OnDeathWithReference?.Invoke(sender);
                Dead();
            }
            else
            {
                StartCoroutine(TurnOffHit());
            }
        }
    }

    public void Stun(float stunDuration)
    {
        if (!isStunned) // Check if the enemy is not already stunned
        {
            StartCoroutine(StunEffect(stunDuration));
        }
    }


    private IEnumerator StunEffect(float stunDuration)
    {
        isStunned = true;
        ChangeColor(Color.blue);

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        ChangeColor(originalColor);
    }

    private void ChangeColor(Color newColor)
    {
        sprite.color = newColor;
    }

    public void Dead()
    {
        if (Random.Range(0f, 1f) <= 0.1f) 
        {
            Instantiate(bulletPickup, transform.position, Quaternion.identity);
        }

        OrganManager.instance.MakeOrgan(rb.position);
        var boom = Instantiate(deathBlood, transform.position, Quaternion.identity);
        boom.Play();
        currentState = enemyState.dead;
        currentHealth = 0;
        levelSystem.AddExperience(levelXP);
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.currentBlood += 2f;
        player.bloodbar.SetBlodd(player.currentBlood);

        gameObject.SetActive(false);
    }

    private IEnumerator TurnOffHit()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        sprite.color = Color.white;

        hit = false;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

}
