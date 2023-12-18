using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeleeAttackManager : MonoBehaviour
{
    public static MeleeAttackManager instance;

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
    private Animator meleeAnimatior;
    private Animator bloodAnimatior;
    private Animator anim;
    private PlayerMovement movement;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        instance = this;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Anchor>().gameObject.GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        meleeAnimatior = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
        bloodAnimatior = this.transform.Find("BloodWeapon").GetComponent<Animator>();
    }
    
    private void Update()
    {
        CheckInput();
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

        if (Input.GetKey(KeyCode.U) && canSwing && movement.CanUseBloodSword() == true)
        {
            bloodSwordChargeTime += Time.deltaTime;
            anim.SetTrigger("BloodSword");
        }

        if (Input.GetKeyUp(KeyCode.U) && (bloodSwordChargeTime > 2) && movement.CanUseBloodSword() == true)
        {
            bloodAnimatior.SetTrigger("BloodMeleeSwipe");
            anim.SetTrigger("BloodSlash");
            bloodSwordChargeTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.U) && (bloodSwordChargeTime < 2))
        {
            bloodSwordChargeTime = 0;
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

    public bool HasPressedKey()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        StartCoroutine(Cooldown());
    }
}
