using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogAnimationController : MonoBehaviour
{
    public Animator animator;
    private bool isAnimating = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        InvokeRepeating("ToggleAnimation", 45f, 1.4f); // Play the animation every 5 seconds starting after 2 seconds
    }

    void ToggleAnimation()
    {
        isAnimating = !isAnimating; // Toggle the boolean variable
        animator.SetBool("cogActivate", isAnimating); // Set the animation parameter based on the boolean variable
    }
}
