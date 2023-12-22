using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stuff")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private AudioManager audioManager;

    [Header("Health")]
    [SerializeField]
    public int maxHealth = 100;
    public int currentHealth;
    public float heal;
    public int healBlockAmount;
    public int healBlockTime;
    public Health_Bar healthbar;
    public Slider blockHealthBar;
    public BlockHealthUI blockHealthScript;
    private bool isHealing = false;


    [Header("Blood")]
    public float maxBlood = 100;
    public float currentBlood;
    public float bloodGain;
    public bool leech = false;
    public Blood_Bar bloodbar;

    [Header("Bullet")]
    [SerializeField]
    public int maxBullets = 3;
    public int currentBullets;


    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;

    [Header("Componenets")]
    private Rigidbody2D body;
    private BoxCollider2D boxcollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public Animator squashStretchAnimator;
    public ParticleSystem dust;
    public ParticleSystem Blood;
    public GameObject Hero;
    public Vector2 Checkpoint;
    public LayerMask whatIsGround;
    private LevelSystemAnimated levelSystemAnimated;
    private PlayerSkills playerSkills;
    private PlayerDeath death;
    private Parry parry;
    private bool isParrying;


    [Header("Variables")]
    public bool canWallJump;
    private float cameraTime = 1f;
    private float cameraTimeCounter;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;
    private bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24;
    public float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private int dashDirection = 1;
    private float dashTimeLeft;
    private float lastDash = -100;
    public float lastImageXpos;
    public float distanceBetweenImages;
    private Coroutine afterImageCoroutine;
    public float FallingThreshold = 0f;
    private bool FacingRight = true;
    public int levelXP;
    public int level;
    private float timeSinceLastFootstep = 0f;
    private float footstepInterval = 0.3f;
    private bool canFlip = true;
    private bool canFlipAir = true;
    private float timeOffGround;
    private float timeOffGroundThreshold = 1f;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;

        body = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        death = GetComponent<PlayerDeath>();
        parry = GetComponent<Parry>();
        vcam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        playerSkills = new PlayerSkills();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        currentBullets = maxBullets;

        currentBlood = 0;
        bloodbar.SetMaxBlood(maxBlood);

        Checkpoint = body.position;
    }

    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }

    private void Update()
    {
        var jumpKey = KeyCode.I;

        if (horizontalInput == 0 && Input.GetKey(KeyCode.W))
        {
            squashStretchAnimator.SetBool("LookingUp", true);
        }
        else
        {
            squashStretchAnimator.SetBool("LookingUp", false);
        }

        if (isDashing)
        {
            return;
        }

        if (MeleeAttackManager.instance.isSwingingSword)
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f && !FacingRight)
        {
            Flip(canFlip);
            dashDirection = 1;
        }
        else if (horizontalInput < -0.01f && FacingRight)
        {
            Flip(canFlip);
            dashDirection = -1;
        }

        if (Input.GetKeyDown(jumpKey))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 8f;
                body.velocity = Vector2.zero;
                squashStretchAnimator.SetTrigger("WallGlide");
            }
            else
            {
                body.gravityScale = 1.5f;
            }

            if (onWall() && !isGrounded() && Input.GetKeyDown(jumpKey) && canWallJump)
            {
                body.gravityScale = 1.5f;
                Jump();
                audioManager.PlaySFX(audioManager.playerJumping);
            }

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
            {
                Jump();
                audioManager.PlaySFX(audioManager.playerJumping);
                jumpBufferCounter = 0;
            }

            if (Input.GetKeyUp(jumpKey) && body.velocity.y > 0)
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                coyoteTimeCounter = 0f;
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.O) && canDash)
        {
            StartCoroutine(Dash());
        }
        CheckDash();

        squashStretchAnimator.SetBool("Run", horizontalInput != 0 && isGrounded());

        if (squashStretchAnimator.GetBool("Run") == true)
        {
            Dust();

            timeSinceLastFootstep += Time.deltaTime;

            if (timeSinceLastFootstep >= footstepInterval)
            {
                //PlayWalkingSFX();
                timeSinceLastFootstep = 0f;
            }
        }
        else
        {
            CancelInvoke("PlayWalkingSFX");
        }

        if (body.velocity.y <= FallingThreshold)
        {
            squashStretchAnimator.SetTrigger("Falling");
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentBlood > maxBlood)
        {
            currentBlood = maxBlood;
        }
        else if (currentBlood <= 0)
        {
            currentBlood = 0;
        }

        if (currentBullets > maxBullets)
        {
            currentBullets = maxBullets;
        }

        if (Input.GetKeyDown(KeyCode.J) && currentHealth != maxHealth && bloodbar.slider.value != 0)
        {
            StartCoroutine(Heal());
        }

        heal = 30;

        if (Input.GetKeyDown(KeyCode.K) && CanUseDeflect() == true)
        {
            Deflect();
        }
    }

    //private void PlayWalkingSFX()
    //{
    //    audioManager.PlaySFX(audioManager.playerLanding);
    //}

    private void Flip(bool flipEnabled)
    {
        if (flipEnabled)
        {
            FacingRight = !FacingRight;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Jump()
    {
        if (onWall() && !isGrounded() && horizontalInput != 0)
        {
            body.velocity = new Vector2(-Mathf.Sign(horizontalInput) * 10, 12);
            wallJumpCooldown = 0;
        }
        else if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            Dust();
            squashStretchAnimator.SetTrigger("Jump");
        }

        if (body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 1f);
        }
    }

    public IEnumerator Dash()
    {
        dashTimeLeft = dashingTime;
        lastDash = Time.time;
        canDash = false;
        isDashing = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        audioManager.PlaySFX(audioManager.playerDashing);

        Vector2 dashDirectionVector = new Vector2(dashDirection, 0f);
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, dashDirectionVector, 1f, wallLayer);
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;

        if (wallHit.collider == null)
        {
            body.velocity = new Vector2(dashDirection * dashingPower, 0f);
            dashTimeLeft -= Time.deltaTime;
            //trail.emitting = true;
            squashStretchAnimator.SetTrigger("Dash");
            yield return new WaitForSeconds(dashingTime);
            //trail.emitting = false;
            body.gravityScale = originalGravity;
            isDashing = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            yield return new WaitForSeconds(dashingCooldown);
        }
        else
        {
            isDashing = false;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
        canDash = true;
    }

    private void CheckDash()
    {
        if (isDashing && afterImageCoroutine == null)
        {
            afterImageCoroutine = StartCoroutine(SpawnAfterImages());
        }
        else if (!isDashing && afterImageCoroutine != null)
        {
            // Stop the coroutine if not dashing
            StopCoroutine(afterImageCoroutine);
            afterImageCoroutine = null;
        }
    }

    private void Deflect()
    {
        parry.StartParry();
    }

    private IEnumerator SpawnAfterImages()
    {
        while (isDashing)
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = transform.position.x;

            yield return new WaitForSeconds(distanceBetweenImages);
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(dashDirection, 0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            audioManager.PlaySFX(audioManager.playerLanding);
            squashStretchAnimator.SetTrigger("Landing");
            Dust();
        }

        if (other.gameObject.CompareTag("Water"))
        {
            Damage(999);
            healthbar.SetHealth(currentHealth);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("BloodParticle"))
        {
            currentBlood += bloodGain;
            bloodbar.SetBlodd(currentBlood);
        }
    }

    IEnumerator Heal()
    {
        if (isHealing)
        {
            yield break;
        }


        isHealing = true;

        squashStretchAnimator.SetTrigger("Heal");
        yield return new WaitForSeconds(.5f);
        currentHealth += Convert.ToInt32(heal);
        healthbar.SetHealth(currentHealth);
        currentBlood -= heal;
        bloodbar.SetBlodd(currentBlood);

        isHealing = false;
    }

    void Dust()
    {
        dust.Play();
    }

    public void Damage(int damage)
    {
        //CameraShake.Instance.ShakeCamera(5f, .2f)
        audioManager.PlaySFX(audioManager.playerTakingDamage);

        if (parry.isParrying == false)
        {
            currentHealth -= damage;
            body.AddForce(new Vector2(500, 250));
            healthbar.SetHealth(currentHealth);
            StartCoroutine(Invunerablility());
            FindObjectOfType<HitStop>().Stop(0.2f);
        }

        if (currentHealth <= 0)
        {
            death.Death();
        }
    }

    private IEnumerator Invunerablility()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public bool CanUseDeflect()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Deflect);
    }

    public bool CanUseBloodSword()
    {
        //return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.BloodSword);
        return true;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        //var expBeforeDeath = levelSystem.GetExperience();

        currentHealth = data.health;
        healthbar.SetHealth(currentHealth);

        currentBlood = data.blood;
        bloodbar.SetBlodd(currentBlood);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;
    }

}
