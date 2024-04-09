using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MeleeAttackManager : MonoBehaviour
{
    private PlayerMovement player;
    public static MeleeAttackManager instance;
    public GameObject bloodswordChargeBar;
    private Slider bloodswordChargeBar_Slider;
    public float movementDuration = 2.0f;
    private float timer = 0.0f;

    public float defaultForce = 300;
    public float upwardsForce = 600;
    public float movementTime = .1f;
    public float cooldownTime = 0.4f;
    private float bloodSwordChargeTime = 0;
    public bool meleeAttack;
    public bool horizontalMeleeAttack;
    private bool bloodAttack;
    public bool canSwing = true;
    public bool canRecieveInput = true;
    public bool inputRecieved;
    public bool isSwingingSword = false;
    private bool canBloodAttack = false;
    private Animator meleeAnimatior;
    private Animator bloodAnimatior;
    private Animator anim;
    private PlayerMovement movement;
    private AudioManager audioManager;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        instance = this;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Anchor>().gameObject.GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        meleeAnimatior = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
        bloodAnimatior = this.transform.Find("BloodWeapon").GetComponent<Animator>();
        bloodswordChargeBar_Slider = bloodswordChargeBar.GetComponent<Slider>();
    }
    
    private void Update()
    {
        CheckInput();

        if (player.currentBlood <= 99)
        {
            canBloodAttack = false;
        }
        else
        {
            canBloodAttack = true;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.U) && canSwing)
        {
            meleeAttack = true;
            isSwingingSword = true; 
            audioManager.PlaySFX(audioManager.playerJumping);
            StartCoroutine(Cooldown());
        }
        else
        {
            meleeAttack = false;
        }

        if (Input.GetKey(KeyCode.U) && canSwing && movement.CanUseBloodSword() == true && canBloodAttack)
        {
            bloodswordChargeBar.SetActive(true);
            timer += Time.deltaTime;
            bloodswordChargeBar_Slider.value = Mathf.Lerp(0, bloodswordChargeBar_Slider.maxValue, timer / 2);

            bloodSwordChargeTime += Time.deltaTime;
            anim.SetTrigger("BloodSword");
        }

        if (Input.GetKeyUp(KeyCode.U) && (bloodSwordChargeTime < 2))
        {
            anim.SetTrigger("Idle");
            bloodSwordChargeTime = 0;
            bloodswordChargeBar_Slider.value = 0;
            timer = 0;
            bloodswordChargeBar.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.U) && (bloodSwordChargeTime > 2) && movement.CanUseBloodSword() == true)
        {
            bloodAnimatior.SetTrigger("BloodMeleeSwipe");
            anim.SetTrigger("BloodSlash");
            bloodSwordChargeTime = 0;
            bloodswordChargeBar_Slider.value = 0;
            timer = 0;
            bloodswordChargeBar.SetActive(false);

            player.currentBlood -= 95;
            player.bloodbar.SetBlodd(player.currentBlood);
        }

        if (Input.GetKeyUp(KeyCode.U) && (bloodSwordChargeTime < 2))
        {
            bloodSwordChargeTime = 0;
            bloodswordChargeBar_Slider.value = 0;
            timer = 0;
            bloodswordChargeBar.SetActive(false);
        }

        if (meleeAttack && Input.GetAxis("Vertical") > 0)
        {
            meleeAnimatior.SetTrigger("UpwardMeleeSwipe");
            anim.SetTrigger("MeleeUp");
        }

        if (meleeAttack && Input.GetAxis("Vertical") < 0 && !movement.isGrounded())
        {
            meleeAnimatior.SetTrigger("DownwardMeleeSwipe");
            anim.SetTrigger("MeleeDown");
        }

        if ((meleeAttack && Input.GetAxis("Vertical") == 0)
            || meleeAttack && Input.GetAxis("Vertical") < 0 && movement.isGrounded())
        {
            meleeAnimatior.SetTrigger("ForwardMeleeSwipe");
            anim.SetTrigger("MeleeHorizontal");
        }
    }

    public IEnumerator Cooldown()
    {
        canSwing = false;
        yield return new WaitForSeconds(cooldownTime);
        isSwingingSword = false;
        horizontalMeleeAttack = false;
        canSwing = true;
    }

    public void Reset()
    {
        StartCoroutine(Cooldown());
    }
}
